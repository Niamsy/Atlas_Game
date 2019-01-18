using System;
using System.Collections.Generic;
using Plants.Plant;
using UnityEngine;
using Variables;

namespace Plants
{
    public class PlantSaver : MonoBehaviour
    {
        public PlantModel plant;
     /*   public Producer producer;
        public Consumer consumer;*/
        private float _LastSavedTime = 0f;
        public int _SaveFrequency = 5;

        [Serializable]
        public struct SaveVector
        {
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public struct SaveQuaternion
        {
            public float w;
            public float x;
            public float y;
            public float z;
        }

        [Serializable]
        public struct StockProd
        {
            public List<Resources>      objects;
            public int                  count;
            public int                  limit;
        }

        [Serializable]
        public struct StockCons
        {
            public List<Resources>      objects;
            public int                  count;
            public int                  limit;
        }

        [Serializable]
        public struct SaveProd
        {
            public int              rate;
            public int              quantity;
            public StockProd        stock;
            public bool             starverd;
            public FloatReference   range;
        }

        [Serializable]
        public struct SaveCons
        {
            public int              rate;
            public int              quantity;
            public StockCons        stock;
            public int              starvationTimeLimit;
            public bool             starverd;
            public FloatReference   range;
        }

        [Serializable]
        public struct PlantData
        {
            public SaveVector       position;
            public SaveQuaternion   rotation;
            public SaveVector       scale;
            public List<SaveProd>   prod;
            public List<SaveCons>   cons;

            public void SetFromTransform(Transform transform)
            {
                // Positiion;
                position.x = transform.position.x;
                position.y = transform.position.y;
                position.z = transform.position.z;

                // Rotation
                rotation.w = transform.rotation.w;
                rotation.x = transform.rotation.x;
                rotation.y = transform.rotation.y;
                rotation.z = transform.rotation.z;

                //Scale
                scale.x = transform.localScale.x;
                scale.y = transform.localScale.y;
                scale.z = transform.localScale.z;
            }

            public void SetProdData(List<Producer> lproducers)
            {
                foreach (Producer lproducer in lproducers)
                {
                    SaveProd cpy = new SaveProd();

                    cpy.quantity = lproducer.Quantity;
                    cpy.rate = lproducer.Rate;
                    cpy.starverd = lproducer.Starverd;
                    cpy.stock.count = lproducer.Stocks.GetCount();
                    cpy.stock.limit = lproducer.Stocks.GetLimit();
                    foreach (Resources obj in lproducer.Stocks.GetObjects())
                    {
                        cpy.stock.objects.Add(obj);
                    }
                    cpy.range = lproducer.Range;
                    prod.Add(cpy);
                }
            }

            public void SetConsData(List<Consumer> lconsumers)
            {
                foreach (Consumer lconsumer in lconsumers)
                {
                    SaveCons cpy = new SaveCons();

                    cpy.quantity = lconsumer.Quantity;
                    cpy.rate = lconsumer.Rate;
                    cpy.starverd = lconsumer.Starverd;
                    cpy.starvationTimeLimit = lconsumer.StarvationTimeLimit;
                    cpy.stock.count = lconsumer.Stocks.GetCount();
                    cpy.stock.limit = lconsumer.Stocks.GetLimit();
                    foreach (Resources obj in lconsumer.Stocks.GetObjects())
                    {
                        cpy.stock.objects.Add(obj);
                    }
                    cpy.range = lconsumer.Range;
                    cons.Add(cpy);
                }
            }

            public List<Producer> GetProducers()
            {
                List<Producer> producers = new List<Producer>();

                foreach (SaveProd pro in prod)
                {
                    Producer copy_producer = new Producer();
                    copy_producer.Quantity = pro.quantity;
                    copy_producer.Rate = pro.rate;
                    copy_producer.Starverd = pro.starverd;
                    copy_producer.Stocks.SetCount(pro.stock.count);
                    copy_producer.Stocks.SetLimit(pro.stock.limit);
                    copy_producer.Stocks.Put(pro.stock.objects);
                    copy_producer.Range = pro.range;
                    producers.Add(copy_producer);
                }       
                return producers;
            }

            public List<Consumer> GetConsumers()
            {
                List<Consumer> consumers = new List<Consumer>();

                foreach (SaveCons con in cons)
                {
                    Consumer copy_consumer = new Consumer();
                    copy_consumer.Quantity = con.quantity;
                    copy_consumer.Rate = con.rate;
                    copy_consumer.Starverd = con.starverd;
                    copy_consumer.StarvationTimeLimit = con.starvationTimeLimit;
                    copy_consumer.Stocks.SetCount(con.stock.count);
                    copy_consumer.Stocks.SetLimit(con.stock.limit);
                    copy_consumer.Stocks.Put(con.stock.objects);
                    copy_consumer.Range = con.range;
                    consumers.Add(copy_consumer);
                }
                return consumers;
            }

            public Vector3 GetPosition()
            {
                return new Vector3(position.x, position.y, position.z); ;
            }

            public Quaternion GetRotation()
            {
                return new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            }

            public Vector3 GetScale()
            {
                return new Vector3(scale.x, scale.y, scale.z);
            }
        }

        public void Save()
        {
            GameControl.control.gameData.PlantData.SetFromTransform(plant.transform);
            GameControl.control.gameData.PlantData.SetProdData(plant.Producer);
            GameControl.control.gameData.PlantData.SetConsData(plant.Consumer);
        }

        private void Awake()
        {
            plant.transform.position = GameControl.control.gameData.PlantData.GetPosition();
            plant.transform.rotation = GameControl.control.gameData.PlantData.GetRotation();
            plant.transform.localScale = GameControl.control.gameData.PlantData.GetScale();
            plant.Producer = GameControl.control.gameData.PlantData.GetProducers();
            plant.Consumer = GameControl.control.gameData.PlantData.GetConsumers();
            _LastSavedTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - _LastSavedTime > _SaveFrequency)
            {
                Save();
                _LastSavedTime = Time.time;
            }
        }
    }
}
