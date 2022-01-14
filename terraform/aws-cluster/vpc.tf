resource "aws_vpc" "kube-vpc" {    
  cidr_block = "10.0.0.0/16"
  tags = {
    Name = "${var.prefix}-vpc"
  }
}

# consultando zonas de disponibilidade
data "aws_availability_zones" "azs" {}
output "az" {
  value = data.aws_availability_zones.azs.names
}

resource "aws_subnet" "subnets" {
  count = 2
  availability_zone = data.aws_availability_zones.azs.names[count.index]
  vpc_id = aws_vpc.kube-vpc.id
  cidr_block = "10.0.${count.index}.0/24"
  map_public_ip_on_launch = true # habilitando criação de ip publico para subnet
  tags = {
    Name = "${var.prefix}-subnet-${count.index + 1}"
  }  
}

resource "aws_internet_gateway" "kube-igw" {
  vpc_id = aws_vpc.kube-vpc.id
  tags = {
      Name = "${var.prefix}-igw"
  }
}

resource "aws_route_table" "kube-routetable" {
  vpc_id = aws_vpc.kube-vpc.id
  route {
      cidr_block = "0.0.0.0/0"
      gateway_id = aws_internet_gateway.kube-igw.id
  }
  tags = {
     Name = "${var.prefix}-routetable"
  }
}

# associando subnets na route table
resource "aws_route_table_association" "kube-routetable-association" {
  count = 2
  route_table_id = aws_route_table.kube-routetable.id
  subnet_id = aws_subnet.subnets.*.id[count.index]
}

