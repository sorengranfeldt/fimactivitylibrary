param
(
	[switch] $IISReset

)

function get-webpageWithAuthN([string]$url,[System.Net.NetworkCredential]$cred=$null){ 
    write-host -foregroundcolor green "Warming up $url"; 
    $wc = new-object net.webclient; 
    $wc.credentials = $cred; 
    #$wc.Headers.Add("user-agent", "PowerShell"); 
    $html = $wc.DownloadString($url); 
    #$html 
}

if (Test-Path "C:\Temp\_FIM-WF-*.log") { Del "C:\Temp\_FIM-WF-*.log" }
(Join-Path $PWD Granfeldt.FIM.ActivityLibrary.dll) | .\Add-AssemblyToGlobalAssemblyCache.ps1
Get-Service FIMService | Restart-Service -Verbose
if ($IISReset) { IISRESET }

#FIM 
$website = "http://localhost/IdentityManagement"
$credentials = [System.Net.CredentialCache]::DefaultCredentials; 
#get-webpageWithAuthN -url $website -cred $credentials
