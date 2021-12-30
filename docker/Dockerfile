FROM nginx:latest

WORKDIR /app

RUN apt-get update && \
    apt-get install vim -y

COPY html/ /usr/share/nginx/html

# é um comando fixo
# CMD é um comando variável que entra como um parâmetro do ENTRYPOINT quando usado em conjunto
# ENTRYPOINT [ "echo", "Hello" ]

# processo que mantém o container em execução
# pode ser substituido na criação do container ex. docker run robertocamara/nginx echo "Roberto Camara"
# CMD [ "echo", "Hello World" ]
 