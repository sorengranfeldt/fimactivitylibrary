param
(
	[switch] $IISReset

)

function Get-WebpageWithAuthN([string]$Url, [System.Net.NetworkCredential] $cred=$null){ 
    Write-Verbose "Warming up $url"
    $wc = New-Object Net.WebClient
    $wc.Credentials = $cred
    $html = $wc.DownloadString($url)
}

if (Test-Path "C:\Temp\_FIM-WF-*.log") { Del "C:\Temp\_FIM-WF-*.log" }
(Join-Path $PWD Granfeldt.FIM.ActivityLibrary.dll) | .\Add-AssemblyToGlobalAssemblyCache.ps1
Get-Service FIMService | Restart-Service -Verbose
if ($IISReset) {
	IISRESET 
	
	$website = "http://localhost/IdentityManagement"
	$credentials = [System.Net.CredentialCache]::DefaultCredentials; 
	get-webpageWithAuthN -url $website -cred $credentials
}
