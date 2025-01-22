using Gostinka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Gostinka.Models;
using Microsoft.EntityFrameworkCore;

namespace Gostinka.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookingsWorkWindow.xaml
    /// </summary>
    public partial class BookingsWorkWindow : Window
    {
        GostinkaContext context;
        public BookingsWorkWindow()
        {
            InitializeComponent();

            context = new GostinkaContext();

            List<Booking> bookings = context.Bookings
                .Include(r => r.Room)
                .ThenInclude(c => c.Category)
                .AsNoTracking()
                .ToList();
            if (bookings.Count > 0)
            {
                bookingsList.ItemsSource = bookings;
            }
        }
    }
}
