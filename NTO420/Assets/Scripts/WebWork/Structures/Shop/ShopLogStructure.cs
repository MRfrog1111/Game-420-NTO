namespace DefaultNamespace
{
    [System.Serializable]
    public struct ShopLogs
    {
        public string comment; 
        public string player_name;
        public string shop_name;
        public ShopChangesLogs resources_changed;
    }
}