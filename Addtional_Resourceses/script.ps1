#pull on client
#user: examRegistryolegsteblev
#pass: QEFaQGxoxu42gz0dI5fLKeYPN/B9KJJg

docker login examregistryolegsteblev.azurecr.io
docker network create --driver transparent --subnet 192.168.3.0/24 --gateway 192.168.3.2  prodnet
docker pull examregistryolegsteblev.azurecr.io/mcr.microsoft.com/windows/servercore:ltsc2019
docker pull examregistryolegsteblev.azurecr.io/contsqlbuilder
docker pull examregistryolegsteblev.azurecr.io/contiisbuilder
docker run --detach -it --name contsqlbuilder --hostname contsqlbuilder --network prodnet --ip 192.168.3.202 examregistryolegsteblev.azurecr.io/contsqlbuilder
docker run --detach -it --name contiisbuilder --hostname contiisbuilder --network prodnet --ip 192.168.3.201 examregistryolegsteblev.azurecr.io/contiisbuilder
