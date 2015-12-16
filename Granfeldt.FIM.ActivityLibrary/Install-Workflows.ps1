# January 30, 2013 | Soren Granfeldt
#	- changed LookupAttributeValue to LookupValueActivity
# december 16, 2015 | soren granfeldt
#	- added ClearSingleValueActivity

PARAM
(
	[string] $AssemblyName = "Granfeldt.FIM.ActivityLibrary.dll",
	[switch] $CreateCodeRunActivity,
	[switch] $CreateLookupValueActivity,
	[switch] $CreateCopyValuesActivity,
	[switch] $CreateCreateObjectActivity,
	[switch] $CreateClearSingleValueActivity,
	[switch] $CreateDeleteObjectActivity
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

	if ($CreateCodeRunActivity)
	{
		$Params = @{ `
			DisplayName = 'Code Activity'
			Description = 'Compile and execute C# code as an activity'
			ActivityName = "$ManifestModule.CodeRunActivity"
			TypeName = "$ManifestModule.WebUIs.CodeRunActivitySettingsPart"
			IsActionActivity = $true
			AssemblyName = $LoadedAssembly.Fullname
		}
		$Params
		.\New-FIMActivityInformationConfigurationObject.ps1 @Params
	}

	if ($CreateLookupValueActivity)
	{
		$Params = @{ `
			DisplayName = 'Lookup Attribute Value'
			Description = 'Using XPath query looks up value in FIM Service'
			ActivityName = "$ManifestModule.LookupValueActivity"
			TypeName = "$ManifestModule.WebUIs.LookupValueActivitySettingsPart"
			IsActionActivity = $true
			AssemblyName = $LoadedAssembly.Fullname
		}
		$Params
		.\New-FIMActivityInformationConfigurationObject.ps1 @Params
	}

	if ($CreateCopyValuesActivity)
	{
		$Params = @{ `
			DisplayName = 'Copy Values'
			Description = 'Copies values to target or enumerated object'
			ActivityName = "$ManifestModule.CopyValuesActivity"
			TypeName = "$ManifestModule.WebUIs.CopyValuesActivitySettingsPart"
			IsActionActivity = $true
			AssemblyName = $LoadedAssembly.Fullname
		}
		$Params
		.\New-FIMActivityInformationConfigurationObject.ps1 @Params
	}

	if ($CreateCreateObjectActivity)
	{
		$Params = @{ `
			DisplayName = 'Create Object'
			Description = 'Creates new object with initial values'
			ActivityName = "$ManifestModule.CreateObjectActivity"
			TypeName = "$ManifestModule.WebUIs.CreateObjectActivitySettingsPart"
			IsActionActivity = $true
			AssemblyName = $LoadedAssembly.Fullname
		}
		$Params
		.\New-FIMActivityInformationConfigurationObject.ps1 @Params
	}

	if ($CreateDeleteObjectActivity)
	{
		$Params = @{ `
			DisplayName = 'Delete Object'
			Description = 'Deletes an object'
			ActivityName = "$ManifestModule.DeleteObjectActivity"
			TypeName = "$ManifestModule.WebUIs.DeleteObjectActivitySettingsPart"
			IsActionActivity = $true
			AssemblyName = $LoadedAssembly.Fullname
		}
		$Params
		.\New-FIMActivityInformationConfigurationObject.ps1 @Params
	}

	if ($CreateClearSingleValueActivity)
	{
		$Params = @{ `
			DisplayName = 'Clear Single Value'
			Description = 'Clears a single value attribute'
			ActivityName = "$ManifestModule.ClearSingleValuedAttributeActivity"
			TypeName = "$ManifestModule.WebUIs.ClearSingleValuedAttributeActivitySettingsPart"
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