using StudentManagement.API.Models;

namespace StudentManagement.API.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // Criar o banco de dados
        context.Database.EnsureCreated();

        // Verificar se já existe dados
        if (context.Students.Any())
        {
            return; // DB já foi populado
        }

        // Seed de usuários
        var users = new User[]
        {
            new User { Id = 1, Username = "admin", Password = "admin123" },
            new User { Id = 2, Username = "user", Password = "user123" }
        };

        context.Users.AddRange(users);

        // Seed de estudantes do CSV
        var students = new Student[]
        {
            new Student { Id = 1, Nome = "Alice", Idade = 10, Serie = 5, NotaMedia = 8.5, Endereco = "123 Main St", NomePai = "John Doe", NomeMae = "Jane Doe", DataNascimento = new DateTime(2013, 5, 15) },
            new Student { Id = 2, Nome = "Bob", Idade = 11, Serie = 6, NotaMedia = 7.2, Endereco = "456 Oak St", NomePai = "Bob Smith", NomeMae = "Alice Smith", DataNascimento = new DateTime(2012, 8, 21) },
            new Student { Id = 3, Nome = "Charlie", Idade = 9, Serie = 4, NotaMedia = 9.0, Endereco = "789 Pine St", NomePai = "Charlie Brown", NomeMae = "Lucy Brown", DataNascimento = new DateTime(2014, 2, 10) },
            new Student { Id = 4, Nome = "David", Idade = 10, Serie = 5, NotaMedia = 8.8, Endereco = "101 Cedar St", NomePai = "David Johnson", NomeMae = "Emily Johnson", DataNascimento = new DateTime(2013, 7, 18) },
            new Student { Id = 5, Nome = "Emma", Idade = 11, Serie = 6, NotaMedia = 7.5, Endereco = "202 Elm St", NomePai = "Michael White", NomeMae = "Sophia White", DataNascimento = new DateTime(2012, 10, 5) },
            new Student { Id = 6, Nome = "Frank", Idade = 9, Serie = 4, NotaMedia = 9.2, Endereco = "303 Maple St", NomePai = "Frank Williams", NomeMae = "Grace Williams", DataNascimento = new DateTime(2014, 1, 22) },
            new Student { Id = 7, Nome = "Grace", Idade = 10, Serie = 5, NotaMedia = 8.0, Endereco = "404 Birch St", NomePai = "George Taylor", NomeMae = "Olivia Taylor", DataNascimento = new DateTime(2013, 4, 30) },
            new Student { Id = 8, Nome = "Henry", Idade = 11, Serie = 6, NotaMedia = 7.8, Endereco = "505 Spruce St", NomePai = "Henry Moore", NomeMae = "Lily Moore", DataNascimento = new DateTime(2012, 9, 14) },
            new Student { Id = 9, Nome = "Isabel", Idade = 9, Serie = 4, NotaMedia = 9.5, Endereco = "606 Walnut St", NomePai = "Isaac Davis", NomeMae = "Ava Davis", DataNascimento = new DateTime(2014, 3, 7) },
            new Student { Id = 10, Nome = "Jack", Idade = 10, Serie = 5, NotaMedia = 7.9, Endereco = "707 Sycamore St", NomePai = "Jack Smith", NomeMae = "Emma Smith", DataNascimento = new DateTime(2013, 6, 19) },
            new Student { Id = 11, Nome = "Katherine", Idade = 11, Serie = 6, NotaMedia = 8.3, Endereco = "808 Cedar St", NomePai = "James Martin", NomeMae = "Sophia Martin", DataNascimento = new DateTime(2012, 11, 28) },
            new Student { Id = 12, Nome = "Liam", Idade = 9, Serie = 4, NotaMedia = 9.1, Endereco = "909 Oak St", NomePai = "Liam Turner", NomeMae = "Ella Turner", DataNascimento = new DateTime(2014, 2, 1) },
            new Student { Id = 13, Nome = "Mia", Idade = 10, Serie = 5, NotaMedia = 8.7, Endereco = "1010 Maple St", NomePai = "Ryan Brown", NomeMae = "Mia Brown", DataNascimento = new DateTime(2013, 5, 12) },
            new Student { Id = 14, Nome = "Nathan", Idade = 11, Serie = 6, NotaMedia = 7.4, Endereco = "1111 Birch St", NomePai = "Nathan Harris", NomeMae = "Eva Harris", DataNascimento = new DateTime(2012, 8, 3) },
            new Student { Id = 15, Nome = "Olivia", Idade = 9, Serie = 4, NotaMedia = 9.3, Endereco = "1212 Pine St", NomePai = "Daniel Green", NomeMae = "Olivia Green", DataNascimento = new DateTime(2014, 1, 9) },
            new Student { Id = 16, Nome = "Peter", Idade = 10, Serie = 5, NotaMedia = 8.4, Endereco = "1313 Elm St", NomePai = "Peter Clark", NomeMae = "Ava Clark", DataNascimento = new DateTime(2013, 4, 18) },
            new Student { Id = 17, Nome = "Quinn", Idade = 11, Serie = 6, NotaMedia = 7.1, Endereco = "1414 Cedar St", NomePai = "Quinn Davis", NomeMae = "Grace Davis", DataNascimento = new DateTime(2012, 9, 27) },
            new Student { Id = 18, Nome = "Rachel", Idade = 9, Serie = 4, NotaMedia = 9.4, Endereco = "1515 Walnut St", NomePai = "Richard White", NomeMae = "Rachel White", DataNascimento = new DateTime(2014, 2, 14) },
            new Student { Id = 19, Nome = "Sam", Idade = 10, Serie = 5, NotaMedia = 8.6, Endereco = "1616 Sycamore St", NomePai = "Sam Turner", NomeMae = "Emily Turner", DataNascimento = new DateTime(2013, 6, 6) },
            new Student { Id = 20, Nome = "Tristan", Idade = 11, Serie = 6, NotaMedia = 7.7, Endereco = "1717 Spruce St", NomePai = "Tristan Harris", NomeMae = "Lily Harris", DataNascimento = new DateTime(2012, 11, 23) },
            new Student { Id = 21, Nome = "Uma", Idade = 9, Serie = 4, NotaMedia = 9.6, Endereco = "1818 Maple St", NomePai = "Uma Smith", NomeMae = "Sophia Smith", DataNascimento = new DateTime(2014, 3, 30) },
            new Student { Id = 22, Nome = "Victor", Idade = 10, Serie = 5, NotaMedia = 8.2, Endereco = "1919 Oak St", NomePai = "Victor Martin", NomeMae = "Ella Martin", DataNascimento = new DateTime(2013, 5, 24) },
            new Student { Id = 23, Nome = "Wendy", Idade = 11, Serie = 6, NotaMedia = 7.0, Endereco = "2020 Pine St", NomePai = "Wendy Brown", NomeMae = "Michael Brown", DataNascimento = new DateTime(2012, 10, 10) },
            new Student { Id = 24, Nome = "Xander", Idade = 9, Serie = 4, NotaMedia = 9.7, Endereco = "2121 Birch St", NomePai = "Xander Turner", NomeMae = "Sophia Turner", DataNascimento = new DateTime(2014, 1, 17) },
            new Student { Id = 25, Nome = "Yara", Idade = 10, Serie = 5, NotaMedia = 8.1, Endereco = "2222 Elm St", NomePai = "Yara Davis", NomeMae = "John Davis", DataNascimento = new DateTime(2013, 4, 4) },
            new Student { Id = 26, Nome = "Zane", Idade = 11, Serie = 6, NotaMedia = 7.3, Endereco = "2323 Cedar St", NomePai = "Zane Harris", NomeMae = "Lily Harris", DataNascimento = new DateTime(2012, 9, 8) },
            new Student { Id = 27, Nome = "Aaron", Idade = 9, Serie = 4, NotaMedia = 9.8, Endereco = "2424 Walnut St", NomePai = "Aaron Smith", NomeMae = "Sophia Smith", DataNascimento = new DateTime(2014, 2, 21) },
            new Student { Id = 28, Nome = "Bella", Idade = 10, Serie = 5, NotaMedia = 8.9, Endereco = "2525 Sycamore St", NomePai = "Bella Martin", NomeMae = "Ella Martin", DataNascimento = new DateTime(2013, 6, 14) },
            new Student { Id = 29, Nome = "Carlos", Idade = 11, Serie = 6, NotaMedia = 7.6, Endereco = "2626 Spruce St", NomePai = "Carlos Turner", NomeMae = "Emily Turner", DataNascimento = new DateTime(2012, 11, 5) },
            new Student { Id = 30, Nome = "Diana", Idade = 9, Serie = 4, NotaMedia = 9.9, Endereco = "2727 Maple St", NomePai = "Diana White", NomeMae = "Michael White", DataNascimento = new DateTime(2014, 3, 18) },
            new Student { Id = 31, Nome = "Ethan", Idade = 10, Serie = 5, NotaMedia = 8.8, Endereco = "2828 Oak St", NomePai = "Ethan Brown", NomeMae = "Sophia Brown", DataNascimento = new DateTime(2013, 4, 23) },
            new Student { Id = 32, Nome = "Fiona", Idade = 11, Serie = 6, NotaMedia = 7.5, Endereco = "2929 Pine St", NomePai = "Fiona Harris", NomeMae = "John Harris", DataNascimento = new DateTime(2012, 10, 16) },
            new Student { Id = 33, Nome = "Gavin", Idade = 9, Serie = 4, NotaMedia = 9.2, Endereco = "3030 Birch St", NomePai = "Gavin Smith", NomeMae = "Olivia Smith", DataNascimento = new DateTime(2014, 1, 3) },
            new Student { Id = 34, Nome = "Holly", Idade = 10, Serie = 5, NotaMedia = 8.0, Endereco = "3131 Cedar St", NomePai = "Holly Davis", NomeMae = "Daniel Davis", DataNascimento = new DateTime(2013, 5, 29) },
            new Student { Id = 35, Nome = "Ian", Idade = 11, Serie = 6, NotaMedia = 7.8, Endereco = "3232 Elm St", NomePai = "Ian Turner", NomeMae = "Sophia Turner", DataNascimento = new DateTime(2012, 9, 20) },
            new Student { Id = 36, Nome = "Jenna", Idade = 9, Serie = 4, NotaMedia = 9.5, Endereco = "3333 Sycamore St", NomePai = "Jenna Martin", NomeMae = "Ella Martin", DataNascimento = new DateTime(2014, 2, 26) },
            new Student { Id = 37, Nome = "Kevin", Idade = 10, Serie = 5, NotaMedia = 8.4, Endereco = "3434 Spruce St", NomePai = "Kevin Harris", NomeMae = "Lily Harris", DataNascimento = new DateTime(2013, 6, 9) },
            new Student { Id = 38, Nome = "Lila", Idade = 11, Serie = 6, NotaMedia = 7.2, Endereco = "3535 Maple St", NomePai = "Lila White", NomeMae = "Michael White", DataNascimento = new DateTime(2012, 8, 14) },
            new Student { Id = 39, Nome = "Mark", Idade = 9, Serie = 4, NotaMedia = 9.3, Endereco = "3636 Oak St", NomePai = "Mark Brown", NomeMae = "Sophia Brown", DataNascimento = new DateTime(2014, 1, 12) },
            new Student { Id = 40, Nome = "Nina", Idade = 10, Serie = 5, NotaMedia = 8.7, Endereco = "3737 Pine St", NomePai = "Nina Smith", NomeMae = "Olivia Smith", DataNascimento = new DateTime(2013, 5, 17) },
            new Student { Id = 41, Nome = "Oscar", Idade = 11, Serie = 6, NotaMedia = 7.9, Endereco = "3838 Birch St", NomePai = "Oscar Turner", NomeMae = "Emily Turner", DataNascimento = new DateTime(2012, 9, 30) },
            new Student { Id = 42, Nome = "Paula", Idade = 9, Serie = 4, NotaMedia = 9.4, Endereco = "3939 Elm St", NomePai = "Paula Harris", NomeMae = "John Harris", DataNascimento = new DateTime(2014, 3, 11) },
            new Student { Id = 43, Nome = "Quincy", Idade = 10, Serie = 5, NotaMedia = 8.1, Endereco = "4040 Cedar St", NomePai = "Quincy Davis", NomeMae = "Daniel Davis", DataNascimento = new DateTime(2013, 4, 1) },
            new Student { Id = 44, Nome = "Ruby", Idade = 11, Serie = 6, NotaMedia = 7.7, Endereco = "4141 Sycamore St", NomePai = "Ruby Martin", NomeMae = "Ella Martin", DataNascimento = new DateTime(2012, 11, 30) },
            new Student { Id = 45, Nome = "Steve", Idade = 9, Serie = 4, NotaMedia = 9.6, Endereco = "4242 Spruce St", NomePai = "Steve White", NomeMae = "Michael White", DataNascimento = new DateTime(2014, 2, 4) },
            new Student { Id = 46, Nome = "Tina", Idade = 10, Serie = 5, NotaMedia = 8.3, Endereco = "4343 Maple St", NomePai = "Tina Brown", NomeMae = "Sophia Brown", DataNascimento = new DateTime(2013, 5, 8) },
            new Student { Id = 47, Nome = "Ursula", Idade = 11, Serie = 6, NotaMedia = 7.1, Endereco = "4444 Oak St", NomePai = "Ursula Smith", NomeMae = "Olivia Smith", DataNascimento = new DateTime(2012, 8, 27) },
            new Student { Id = 48, Nome = "Vince", Idade = 9, Serie = 4, NotaMedia = 9.1, Endereco = "4545 Pine St", NomePai = "Vince Turner", NomeMae = "Emily Turner", DataNascimento = new DateTime(2014, 1, 20) },
            new Student { Id = 49, Nome = "Wes", Idade = 10, Serie = 5, NotaMedia = 8.9, Endereco = "4646 Birch St", NomePai = "Wes Harris", NomeMae = "Lily Harris", DataNascimento = new DateTime(2013, 4, 27) },
            new Student { Id = 50, Nome = "Xena", Idade = 11, Serie = 6, NotaMedia = 7.4, Endereco = "4747 Elm St", NomePai = "Xena Davis", NomeMae = "John Davis", DataNascimento = new DateTime(2012, 9, 13) },
            new Student { Id = 51, Nome = "Yvonne", Idade = 9, Serie = 4, NotaMedia = 9.7, Endereco = "4848 Sycamore St", NomePai = "Yvonne Martin", NomeMae = "Ella Martin", DataNascimento = new DateTime(2014, 3, 24) },
            new Student { Id = 52, Nome = "Zach", Idade = 10, Serie = 5, NotaMedia = 8.2, Endereco = "4949 Spruce St", NomePai = "Zach White", NomeMae = "Michael White", DataNascimento = new DateTime(2013, 5, 3) }
        };

        context.Students.AddRange(students);
        context.SaveChanges();
    }
}
