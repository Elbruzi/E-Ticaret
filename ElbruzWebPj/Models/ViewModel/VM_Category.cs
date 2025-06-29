using ElbruzWebPj.Models.MVVM;

namespace ElbruzWebPj.Models.ViewModel
{
    public class VM_Category
    {

        public Category SingleCategory { get; set; } // Yeni kategori eklemek için

        public IEnumerable<Category> IECategory { get; set; } // Kategorilerin listesi

        public string MainCategoryFinder { get; set; } // Kategorilerin listesi

    }

}
