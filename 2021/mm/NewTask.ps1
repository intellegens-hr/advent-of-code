param($number)
new-item "Inputs/Task$number" -itemtype directory
new-item "Solutions/Task$number" -itemtype directory
new-item "Inputs/Task$number/Example.txt" -itemtype file
new-item "Inputs/Task$number/Input.txt" -itemtype file
Copy-Item ".\Solutions\TaskEmpty.cs" -Destination ".\Solutions\Task$number\"