# .agent/skills/check-dependencies.ps1
Write-Host "Running Cross-Module Dependency Check..." -ForegroundColor Cyan

if (-not (Test-Path "Modules")) {
    Write-Host "Error: 'Modules' directory not found. Please run this from the solution root." -ForegroundColor Red
    exit 1
}

$violations = 0
$projects = Get-ChildItem -Path "Modules" -Filter "*.csproj" -Recurse

foreach ($project in $projects) {
    $fullPath = $project.FullName
    # Extract module name from the path
    if ($fullPath -match "Modules[\\/]([^\\/]+)") {
        $currentModule = $Matches[1]
    } else {
        continue
    }

    # Get references using dotnet CLI
    $references = dotnet list $fullPath reference
    
    foreach ($line in $references) {
        $trimmedLine = $line.Trim()
        if ($trimmedLine -like "*Modules*") {
            # Extract target module name
            if ($trimmedLine -match "Modules[\\/]([^\\/]+)") {
                $refModule = $Matches[1]
                
                # Check for cross-module violation
                if ($currentModule -ne $refModule) {
                    Write-Host "❌ ARCHITECTURE VIOLATION: Cross-Module Reference Detected" -ForegroundColor Red
                    Write-Host "   Source Module : $currentModule"
                    Write-Host "   Source Project: $fullPath"
                    Write-Host "   Illegal Target: $refModule"
                    Write-Host "   Reference     : $trimmedLine"
                    Write-Host "--------------------------------------------------------"
                    $violations++
                }
            }
        }
    }
}

if ($violations -gt 0) {
    Write-Host "❌ Check failed: Found $violations architectural violation(s)." -ForegroundColor Red
    exit 1
} else {
    Write-Host "✅ All clear. No cross-module references found." -ForegroundColor Green
    exit 0
}
