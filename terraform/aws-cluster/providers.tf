terraform {
  required_version = ">=1.1.3" # versão do terraform
  required_providers {
    aws = ">=3.71.0"
    local = ">=2.1.0"
  }
}

provider "aws" {
  region = "us-east-1"
}