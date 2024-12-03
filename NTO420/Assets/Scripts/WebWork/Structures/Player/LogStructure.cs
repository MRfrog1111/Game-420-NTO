namespace DefaultNamespace
{
    [System.Serializable]
    public struct PlayerLogs
    {
        public string comment; 
        public string player_name;
        public PlayerChangesLogs resources_changed;
    }
}