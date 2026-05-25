using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BookCatalog
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
    }

    public class Form1 : Form
    {
        private DataGridView dataGridViewBooks;
        private TextBox textBoxFilterAuthor;
        private TextBox textBoxFilterYear;
        private Button buttonFilter;
        private Button buttonSortByTitle;
        private Button buttonSortByPrice;

        private List<Book> allBooks = new List<Book>();

        public Form1()
        {
            this.Text = "Справочник «Книги»";
            this.Size = new Size(820, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            CreateControls();
            LoadData();
            DisplayBooks(allBooks);
            WireUpEvents();
        }

        private void CreateControls()
        {
            dataGridViewBooks = new DataGridView
            {
                Location = new Point(12, 12),
                Size = new Size(780, 300),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblAuthor = new Label { Text = "Автор:", Location = new Point(12, 330), Size = new Size(60, 20) };
            textBoxFilterAuthor = new TextBox { Location = new Point(80, 327), Size = new Size(150, 22) };

            var lblYear = new Label { Text = "Год:", Location = new Point(250, 330), Size = new Size(40, 20) };
            textBoxFilterYear = new TextBox { Location = new Point(295, 327), Size = new Size(80, 22) };

            buttonFilter = new Button
            {
                Text = "Применить фильтр",
                Location = new Point(400, 325),
                Size = new Size(140, 28)
            };

            buttonSortByTitle = new Button
            {
                Text = "Сортировать по названию",
                Location = new Point(12, 370),
                Size = new Size(200, 28)
            };

            buttonSortByPrice = new Button
            {
                Text = "Сортировать по цене",
                Location = new Point(230, 370),
                Size = new Size(200, 28)
            };

            this.Controls.AddRange(new Control[]
            {
                dataGridViewBooks,
                lblAuthor, textBoxFilterAuthor,
                lblYear, textBoxFilterYear,
                buttonFilter,
                buttonSortByTitle, buttonSortByPrice
            });
        }

        private void LoadData()
        {
            allBooks = new List<Book>
            {
                new Book { ID = 1, Title = "Война и мир", Author = "Толстой", Year = 1869, Price = 500 },
                new Book { ID = 2, Title = "Преступление и наказание", Author = "Достоевский", Year = 1866, Price = 350 },
                new Book { ID = 3, Title = "Мастер и Маргарита", Author = "Булгаков", Year = 1967, Price = 400 },
                new Book { ID = 4, Title = "Тихий Дон", Author = "Шолохов", Year = 1940, Price = 450 },
                new Book { ID = 5, Title = "Лолита", Author = "Набоков", Year = 1955, Price = 300 },
                new Book { ID = 6, Title = "Анна Каренина", Author = "Толстой", Year = 1877, Price = 420 },
                new Book { ID = 7, Title = "Идиот", Author = "Достоевский", Year = 1869, Price = 380 }
            };
        }

        private void DisplayBooks(List<Book> books)
        {
            dataGridViewBooks.DataSource = null;
            dataGridViewBooks.DataSource = books;
        }

        private void WireUpEvents()
        {
            buttonFilter.Click += (s, e) =>
            {
                string author = textBoxFilterAuthor.Text.Trim().ToLower();
                int.TryParse(textBoxFilterYear.Text.Trim(), out int year);
                bool hasYear = !string.IsNullOrWhiteSpace(textBoxFilterYear.Text.Trim());

                var filtered = allBooks.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(author))
                    filtered = filtered.Where(b => b.Author.ToLower().Contains(author));

                if (hasYear)
                    filtered = filtered.Where(b => b.Year == year);

                DisplayBooks(filtered.ToList());
            };

            buttonSortByTitle.Click += (s, e) =>
            {
                DisplayBooks(allBooks.OrderBy(b => b.Title).ToList());
            };

            buttonSortByPrice.Click += (s, e) =>
            {
                DisplayBooks(allBooks.OrderBy(b => b.Price).ToList());
            };
        }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}