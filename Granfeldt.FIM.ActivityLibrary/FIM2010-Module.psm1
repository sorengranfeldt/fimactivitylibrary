# April 17, 2012 | Soren Granfeldt (soren@granfeldt.dk)
#	- initial version

#----------------------------------------------------------------------------------------------------------
function Get-FIMResource
{
    PARAM($Filter, $Uri = $DefaultUri)
    END
    {
        $resources = Export-FIMConfig -CustomConfig $Filter -Uri $Uri
        $resources
    }
}
#----------------------------------------------------------------------------------------------------------
function Get-FIMResourceObjectID
{
    PARAM($Filter, $Uri, $FilterObjectType)
    END
    {
        $resources = Export-FIMConfig -CustomConfig $Filter -Uri $Uri
	if ($FilterObjectType)
	{
		$resources = $resources | ? { $_.ResourceManagementObject.ObjectType -imatch $FilterObjectType}
	}
	[Microsoft.ResourceManagement.Automation.ObjectModel.ExportObject] $ObjectID = $resources | Select -First 1
	$ObjectID.ResourceManagementObject.ObjectIdentifier
    }
}

#----------------------------------------------------------------------------------------------------------
 Function Set-FIMAttribute
 {
	Param($object, $attributeName, $attributeValue)
	End
	{
		$importChange = New-Object Microsoft.ResourceManagement.Automation.ObjectModel.ImportChange
		$importChange.Operation = 1
		$importChange.AttributeName = $attributeName
		$importChange.AttributeValue = $attributeValue
		$importChange.FullyResolved = 1
		$importChange.Locale = "Invariant"
		If ($object.Changes -eq $null) {$object.Changes = (,$importChange)}
		Else {$object.Changes += $importChange}
	}
} 
#----------------------------------------------------------------------------------------------------------
 Function New-FIMImportObject
 {
	Param($objectType)
	End
	{
		$newObject = New-Object Microsoft.ResourceManagement.Automation.ObjectModel.ImportObject
		$newObject.ObjectType = $objectType
		$newObject.SourceObjectIdentifier = [System.Guid]::NewGuid().ToString()
		$newObject
	} 
 }
#----------------------------------------------------------------------------------------------------------
Function Add-FIMMultiValue
{
	Param($object, $attributeName, $attributeValue)
	End
	{
		$importChange = New-Object Microsoft.ResourceManagement.Automation.ObjectModel.ImportChange
		$importChange.Operation = 0
		$importChange.AttributeName = $attributeName
		$importChange.AttributeValue = $attributeValue
		$importChange.FullyResolved = 1
		$importChange.Locale = "Invariant"
		If ($object.Changes -eq $null) {$object.Changes = (,$importChange)}
		Else {$object.Changes += $importChange}
		}
}
