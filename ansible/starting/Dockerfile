FROM ubuntu:18.04

RUN apt-get update && apt-get install vim ssh ansible -y
# trocando a senha do usuário root
RUN echo 'root:admin' | chpasswd 
# permitindo login remoto como root  
RUN sed -i 's/#PermitRootLogin prohibit-password/PermitRootLogin yes/' /etc/ssh/sshd_config
# habilitando a porta 22
RUN sed -i 's/#Port 22/Port 22/' /etc/ssh/sshd_config
# habilitando autenticação
RUN sed -i 's/#PubkeyAuthentication/PubkeyAuthentication/' /etc/ssh/sshd_config
RUN sed -i 's/#PasswordAuthentication/PasswordAuthentication/' /etc/ssh/sshd_config
