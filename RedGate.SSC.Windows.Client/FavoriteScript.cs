namespace RedGate.SSC.Windows.Client
{
    public class FavoriteScript
    {
        private readonly int m_ContentItemId;
        private readonly string m_FavoritedDate;

        internal FavoriteScript(int contentItemId, string favoritedDate)
        {
            m_ContentItemId = contentItemId;
            m_FavoritedDate = favoritedDate;
        }

        public int ContentItemId
        {
            get { return m_ContentItemId; }
        }

        public string FavoritedDate
        {
            get { return m_FavoritedDate; }
        }
    }
}