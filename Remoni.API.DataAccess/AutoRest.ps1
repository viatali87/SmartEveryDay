$err = $null;
$swaggerfilepath = "swagger.json"
$autorestPath = Resolve-Path "..\tools\autorest.0.17.3\tools\AutoRest.exe" -ErrorVariable err
$outputdir = ".\Client"
$outputdirAbs = Resolve-Path $outputdir
$projectFile = Resolve-Path "Remoni.API.DataAccess.csproj"

If($err -ne $null) {
	Write-Host "Autorest tool could not be found try Restoring Nuget packages for Common solution"
} Else {
	# download swagger file
	$errs = $null
	Invoke-WebRequest http://api.remoni.com/v1 `
		-Headers @{ "accept"="application/json" } `
		-OutFile $swaggerfilepath `
		-ErrorVariable errs
		
	If($errs.Count -Eq 0) {
		# Delete old client
		##Get-ChildItem -Path $outputdirAbs -Include * | remove-Item -recurse
		Remove-Item $outputdirAbs -recurse -ErrorVariable errs
		New-Item -ItemType directory -Path $outputdirAbs

		# Create new client
		& $autorestPath `
			-CodeGenerator CSharp `
			-Modeler Swagger `
			-Input $swaggerfilepath  `
			-Namespace "Remoni.API.DataAccess.Client" `
			-ClientName "RemoniAPIClient" `
			-OutputDirectory $outputdir `
			-UseDateTimeOffset
		
		If($LastExitCode -eq 0) {
			# Update project file
			$xml = [xml](Get-Content $projectFile)
			$itemgroup = $xml.Project.ItemGroup | ? { $_.Compile.Count -gt 0 }
			$filestoremove = $itemgroup | % { $_.Compile } | ? { $_.Include.StartsWith("Client\") } | % { $itemgroup.RemoveChild($_) }

			$idx = ([string]$outputdirAbs).Length
			$clientfiles = Get-ChildItem $outputdirAbs -File -Recurse | % { "Client" + $_.fullName.SubString($idx) }
			$clientfiles | % { 
				$item = $xml.CreateElement('Compile', $xml.DocumentElement.NamespaceURI)
				$item.SetAttribute("Include", $_)
				$itemgroup.AppendChild($item)
			}
			$xml.Save($projectFile)
		} else {
			write-host "Client generation failed, aborting"
		}
	} else {
		write-host "Swagger.json download failed, aborting"
		write-host $error
	}
}