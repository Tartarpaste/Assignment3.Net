using MagicTord_N_SondreTheAPI.Models;
using Microsoft.EntityFrameworkCore;

using (var context2 = new DBContext())
{
    context2.Database.Migrate();
}