using NLayer.Core.UnitOfWorks;

namespace NLayer.Repository.UnitOfWorks
{
    // Burada Core katmanında oluşturduğumuz InitOfWork ara yüzdeki imzalari burdaki class'a implement ederek, imzalarının içini işlevlerini bu class'a yazdık.
    // Bu class memoride tutulan add, remove update vb. işlemlerini veritabanı göndermek (kaydetmek)  için save metodların  işlevleri yazılmıştır.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
