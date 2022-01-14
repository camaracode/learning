output "vpc_id" {
  value = aws_vpc.kube-vpc.id
}

output "subnet_ids" {
  value = aws_subnet.subnets[*].id
}