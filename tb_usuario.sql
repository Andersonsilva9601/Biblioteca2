use biblioteca;
create table TB_USUARIO(
  Id int Primary Key Auto_increment,
  Nome varchar (100),
  Acesso int (11),
  Senha varchar (100),
  Usuario varchar(100)
);
  insert into tb_usuario (Nome, Acesso, Senha, Usuario)
values ("Administrator", "1", "admin", "admin")

insert into tb_usuario (Nome, Acesso, Senha, Usuario)
values ("User", "0", "user", "user")

SELECT * FROM tb_usuario;