# Using a simple templating mechanism for configuring mcollective files

function Write-Template {
    param(
        [string]$inFile,
        [string]$outFile,
        [System.Collections.Hashtable] $variables
    )

    foreach ($key in $variables.Keys){
        New-Variable -Name $key -Value $variables[$key]
    }
    
    $fullScript = [ScriptBlock]::Create("
`$content = [IO.File]::ReadAllText( `$inFile )
Invoke-Expression `"@```"``r``n`$content``r``n```"@`" | Out-File $outFile -Encoding ascii
")

    & $fullScript
}