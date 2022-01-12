# resource = bloco
# local = provider
# file = tipo do provider
# exemplo = nome do recurdo
resource "local_file" "exemplo" {
  filename = "exemplo.txt"
  content = "Roberto Camara - Terraform"
}

