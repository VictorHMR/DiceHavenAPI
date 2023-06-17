CREATE TABLE TB_GRUPO(
ID_GRUPO INT NOT NULL PRIMARY KEY,
DS_GRUPO VARCHAR(100) NOT NULL,
DS_DESCRICAO VARCHAR(200),
FL_ADMIN BOOL NOT NULL DEFAULT 0
);

INSERT INTO TB_GRUPO VALUES(1, 'Admin', 'Pode administrar e acessar todas as funções', true);
INSERT INTO TB_GRUPO VALUES(2, 'Moderador_1', 'Pode administrar todas as funções, com algumas restrições', false);
INSERT INTO TB_GRUPO VALUES(3, 'Comum', 'Pode apenas utilizar o sistema', false);
