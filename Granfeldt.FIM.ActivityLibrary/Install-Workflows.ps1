PARAM
(
	[string] $AssemblyName = "Granfeldt.FIM.ActivityLibrary.dll",
	[switch] $CreateCodeActivity
)

BEGIN
{
	if ( $null -eq ([AppDomain]::CurrentDomain.GetAssemblies() |? { $_.FullName -eq "System.EnterpriseServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" }) ) {
		[System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a") | Out-Null
	}
}

PROCESS
{
	(Join-Path $PWD $AssemblyName) | .\Add-AssemblyToGlobalAssemblyCache.ps1

	$LoadedAssembly = [System.Reflection.Assembly]::LoadFile( (Resolve-Path $AssemblyName).Path )
	$ManifestModule = $LoadedAssembly.ManifestModule -replace '\.dll$'

	if ($CreateCodeActivity)
	{
		$Params = @{ `
			DisplayName = 'Code Activity'
			Description = 'Execute C# code as an activity'
			ActivityName = "$ManifestModule.CodeActivity"
			TypeName = "$ManifestModule.WebUIs.CodeActivitySettingsPart"
			IsActionActivity = $true
			AssemblyName = $LoadedAssembly.Fullname
		}
		$Params
		.\New-FIMActivityInformationConfigurationObject.ps1 @Params
	}

}

END 
{
}