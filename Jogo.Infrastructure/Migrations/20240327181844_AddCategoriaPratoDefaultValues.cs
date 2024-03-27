using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jogo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaPratoDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var sql =
            """
            -- Inserção das 5 categorias principais
            INSERT INTO Categoria (Nome, IdCategoriaPai) VALUES 
            ('Massa', NULL),
            ('Carne', NULL),
            ('Sobremesa', NULL),
            ('Bebida', NULL),
            ('Salada', NULL);

            -- Inserção dos 2 pratos genéricos em cada categoria principal
            INSERT INTO Prato (Nome, IdCategoria) VALUES 
            ('Espaguete', 1),
            ('Lasanha', 1),
            ('Bife', 2),
            ('Frango Grelhado', 2),
            ('Pudim', 3),
            ('Mousse', 3),
            ('Água', 4),
            ('Refrigerante', 4),
            ('Caesar', 5),
            ('Caprese', 5);

            -- Inserção das 2 subcategorias e 2 pratos específicos em cada subcategoria para cada categoria principal

            -- Massa
            INSERT INTO Categoria (Nome, IdCategoriaPai) VALUES 
            ('Massa Recheada', 1),
            ('Massa Seca', 1);

            INSERT INTO Prato (Nome, IdCategoria) VALUES 
            ('Ravioli', 6),
            ('Canelone', 6),
            ('Spaghetti', 7),
            ('Fettuccine Alfredo', 7);

            -- Carne
            INSERT INTO Categoria (Nome, IdCategoriaPai) VALUES 
            ('Carne Vermelha', 2),
            ('Carne Branca', 2);

            INSERT INTO Prato (Nome, IdCategoria) VALUES 
            ('Costela', 8),
            ('Picanha', 8),
            ('Peito de Frango', 9),
            ('Filé Mignon', 9);

            -- Sobremesa
            INSERT INTO Categoria (Nome, IdCategoriaPai) VALUES 
            ('Sorvete', 3),
            ('Torta', 3);

            INSERT INTO Prato (Nome, IdCategoria) VALUES 
            ('Chocolate', 10),
            ('Baunilha', 10),
            ('Limão', 11),
            ('Morango', 11);

            -- Bebida
            INSERT INTO Categoria (Nome, IdCategoriaPai) VALUES 
            ('Alcoólica', 4),
            ('Não Alcoólica', 4);

            INSERT INTO Prato (Nome, IdCategoria) VALUES 
            ('Vinho', 12),
            ('Cerveja', 12),
            ('Água', 13),
            ('Suco', 13);

            -- Salada
            INSERT INTO Categoria (Nome, IdCategoriaPai) VALUES 
            ('Verde', 5),
            ('De Frutas', 5);

            INSERT INTO Prato (Nome, IdCategoria) VALUES 
            ('Caesar', 14),
            ('Caprese', 14),
            ('Frutas', 15),
            ('Mix de Folhas', 15);            
            """;

			migrationBuilder.Sql(sql);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
