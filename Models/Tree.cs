using Newtonsoft.Json;
using System.IO;

namespace MantisGenerator.Models
{
    public class Tree
    {
        public Node Root { get; private set; }

        public Tree()
        {
            Load();
        }

        public bool Delete(Node node)
        {
            return Root.Delete(node);
        }

        public void Save()
        {
            using (StreamWriter writer = new StreamWriter($"storage.json"))
            {
                string json = JsonConvert.SerializeObject(Root);
                writer.Write(json);
            }
        }

        private void Load()
        {
            try
            {
                using (StreamReader reader = new StreamReader($"storage.json"))
                {
                    string json = reader.ReadToEnd();
                    Node restored = JsonConvert.DeserializeObject<Node>(json);
                    Root = restored ?? new Node();
                }
            }
            catch
            {
                Root = new Node();
            }
        }
    }
}
