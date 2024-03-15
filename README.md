# Fileparser

The file parser console application has two functionalities
1. It displays key-value pairs for a given file (current support is for .csv, .json and .xml)
2. Search for a given value in the 'data' folder and print its value.

## Design patterns

1. The support for .json and .xml files are added through strategy pattern.
2. The creation of parsers are done using Factory design pattern.

### PS

The CICD pipeline is setup using Github actions. The testcases will be run automatically as part of every pull request.
