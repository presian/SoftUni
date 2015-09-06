namespace CollectionOfProducts
{
    class CollectionOfProductsMain
    {
        static void Main()
        {
            var productDict = new ProductsDatastructue();
            productDict.Add("bob", 2.5m, "krina");
            productDict.Add("bob", 1.8m, "popeto");
            productDict.Add("oriz", 2.0m, "UncleBence");
            productDict.Add("oriz", 2.8m, "UncleBence");
            productDict.Add("oriz", 2.0m, "lksdjlf");
            productDict.Add("le6ta", 2.0m, "lalal");
            productDict.Add("bob", 1.9m, "tata");

            var productsByPriceRange = productDict.FindByPriceRane(1.9m, 2.0m);
            var productsByTitle = productDict.FindByTitle("le6ta");
            var productsBySupplierAndPrice = productDict.FindBySupplierAndPrice("UncleBence", 2.0m);
            var productsBySupplierAndPriceRange = productDict.FindBySupplierAndPriceRange("UncleBence", 1.9m, 2.1m);
            var productsByTitleAndPrice = productDict.FindByTitleAndPrice("UncleBence", 2.8m);
            var productsByTitleAndPriceRange = productDict.FindByTitleAndPriceRange("oriz", 2.5m, 3.8m);

            var removeResultOne = productDict.Remove(2);
            var removeResultTwo = productDict.Remove(2);
        }
    }
}
