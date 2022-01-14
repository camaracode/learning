terraform {
  required_version = ">=1.1.3" # versão do terraform
  required_providers {
    aws = ">=3.71.0"
    local = ">=2.1.0"
  }
  # configurando s3 para guardar e versionar o arquivo tfstate
  backend "s3" {
      bucket = "kube-clustercamara-bucket"
      key    = "terraform.tfstate"
      region = "us-east-1"
  }
}

provider "aws" {
  region = "us-east-1"
}

module "kube-module-vpc" {
  source = "./modules/vpc"
  prefix = var.prefix
  vpc_cidr_block = var.vpc_cidr_block
}

module "kube-eks" {
  source = "./modules/eks"
  prefix = var.prefix
  vpc_id = module.kube-module-vpc.vpc_id
  cluster_name = var.cluster_name
  retention_days = var.retention_days
  subnet_ids = module.kube-module-vpc.subnet_ids
  desired_size = var.desired_size
  max_size = var.max_size
  min_size = var.min_size
}