using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace PersonnelDepartment
{
    class Workers: List<Worker>, ICloneable
    {
        public void SortByName()
        {

            for (int i = 0; i < this.Count / 2; i++)
            {
                bool swapFlag = false;
                for (int j = i; j < this.Count - i - 1; j++)
                {
                    if (String.Compare(this[j].SecondNameAndInitials, this[j+1].SecondNameAndInitials) == -1)
                    {
                        Swap(j, j + 1);
                        swapFlag = true;
                    }
                }
                for (int j = this.Count - 2 - i; j > i; j--)
                {
                    if (String.Compare(this[j - 1].SecondNameAndInitials, this[j].SecondNameAndInitials) == -1)
                    {
                        Swap(j - 1, j);
                        swapFlag = true;
                    }
                }
                if (!swapFlag)
                { break; }
            }
        }

        public void SortByPosition()
        {
            for (int i = 0; i < this.Count / 2; i++)
            {
                bool swapFlag = false;
                for (int j = i; j < this.Count - i - 1; j++)
                {
                    if (String.Compare(this[j].Position, this[j + 1].Position) == -1)
                    {
                        Swap(j, j + 1);
                        swapFlag = true;
                    }
                }
                for (int j = this.Count - 2 - i; j > i; j--)
                {
                    if (String.Compare(this[j - 1].Position, this[j].Position) == -1)
                    {
                        Swap(j - 1, j);
                        swapFlag = true;
                    }
                }
                if (!swapFlag)
                { break; }
            }
        }

        public void SortByExperience()
        {
            for (int i = 0; i < this.Count / 2; i++)
            {
                bool swapFlag = false;
                for (int j = i; j < this.Count - i - 1; j++)
                {
                    if (this[j].Experience > this[j + 1].Experience)
                    {
                        Swap(j, j + 1);
                        swapFlag = true;
                    }
                }
                for (int j = this.Count - 2 - i; j > i; j--)
                {
                    if (this[j - 1].Experience > this[j].Experience)
                    {
                        Swap(j - 1, j);
                        swapFlag = true;
                    }
                }
                if (!swapFlag)
                { break; }
            }
        }

        private void Swap(int positionA, int positionB)
        {
            if (positionA < this.Count && positionB < this.Count)
            {
                var temp = this[positionA];
                this[positionA] = this[positionB];
                this[positionB] = temp;
        }
        }

        public object Clone()
        {
            var workers = new Workers();
            foreach(Worker worker in this)
            {
                workers.Add(worker.Clone());
            }
            return workers;
        }

        public Worker Search(string secondName)
        {
            foreach(Worker worker in this)
            {
                if(worker.SecondName.ToLower() == secondName.ToLower())
                {
                    return worker;
                }
            }
            return null;
        }

        public static void Save(Workers workers, string path)
        {
            string json = JsonConvert.SerializeObject(workers);
            File.WriteAllText(path, json);
        }

        public static Workers Load(string path)
        {
            try
            {
                using (StreamReader file = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Workers workers = (Workers)serializer.Deserialize(file, typeof(Workers));
                    return workers;
                }
            }
            catch
            {
                throw new JsonException("Не получилось десериализовать файл");
            }
        }
    }
}
