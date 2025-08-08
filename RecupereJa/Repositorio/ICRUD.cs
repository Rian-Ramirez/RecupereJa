using RecupereJa.ViewModel;

namespace RecupereJa.Repository
{
    public interface ICRUD<T>
    {
        public int Criar(T entidade);

        public void Atualizar(T entidade);

        public List<T> BuscarTodos();

        public T BuscarPorId(int id);

        public void Deletar(int id);
        List<ItemViewModel> BuscarItemParaHome();
    }
}
