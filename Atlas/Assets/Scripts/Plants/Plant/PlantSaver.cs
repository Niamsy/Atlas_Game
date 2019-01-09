using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Variables;

namespace Plants
{
    public class PlantSaver<T, U> : MonoBehaviour where T : IResource<T> where U : IResource<U>
    {
        public PlantModel<T, U> plant;
        public Producer<T> producer;
        public Consumer<U> consumer;
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
            public List<T>     objects;
            public int         count;
            public int         limit;
        }

        [Serializable]
        public struct StockCons
        {
            public List<U> objects;
            public int count;
            public int limit;
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
            public SaveProd         prod;
            public SaveCons         cons;

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

            public void SetProdData(Producer<T> lproducer)
            {
                prod.quantity = lproducer.Quantity;
                prod.rate = lproducer.Rate;
                prod.starverd = lproducer.Starverd;
                prod.stock.count = lproducer.Stock.GetCount();
                prod.stock.limit = lproducer.Stock.GetLimit();
                foreach (T obj in lproducer.Stock.GetObjects())
                {
                    prod.stock.objects.Add(obj);
                }
                prod.range = lproducer.Range;
            }

            public void SetConsData(Consumer<U> lconsumer)
            {
                cons.quantity = lconsumer.Quantity;
                cons.rate = lconsumer.Rate;
                cons.starverd = lconsumer.Starverd;
                cons.starvationTimeLimit = lconsumer.StarvationTimeLimit;
                cons.stock.count = lconsumer.Stock.GetCount();
                cons.stock.limit = lconsumer.Stock.GetLimit();
                foreach (U obj in lconsumer.Stock.GetObjects())
                {
                    cons.stock.objects.Add(obj);
                }
                cons.range = lconsumer.Range;
            }

            public Producer<T> GetProducer()
            {
                Producer<T> copy_producer = new Producer<T>();
                copy_producer.Quantity = prod.quantity;
                copy_producer.Rate = prod.rate;
                copy_producer.Starverd = prod.starverd;
                copy_producer.Stock.SetCount(prod.stock.count);
                copy_producer.Stock.SetLimit(prod.stock.limit);
                copy_producer.Stock.Put(prod.stock.objects);
                copy_producer.Range = prod.range;
                return copy_producer;
            }

            public Consumer<U> GetConsumer()
            {
                Consumer<U> copy_consumer = new Consumer<U>();
                copy_consumer.Quantity = cons.quantity;
                copy_consumer.Rate = cons.rate;
                copy_consumer.Starverd = cons.starverd;
                copy_consumer.StarvationTimeLimit = cons.starvationTimeLimit;
                copy_consumer.Stock.SetCount(cons.stock.count);
                copy_consumer.Stock.SetLimit(cons.stock.limit);
                copy_consumer.Stock.Put(cons.stock.objects);
                copy_consumer.Range = cons.range;
                return copy_consumer;
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
            //GameControl.control.gameData.PlantData.SetFromTransform(plant.transform);
            //GameControl.control.gameData.PlantData.SetProdData(producer);
            //GameControl.control.gameData.PlantData.SetConsData(consumer);
        }

        private void Awake()
        {
            //plant.transform.position = GameControl.control.gameData.PlantData.GetPosition();
            //plant.transform.rotation = GameControl.control.gameData.PlantData.GetRotation();
            //plant.transform.localScale = GameControl.control.gameData.PlantData.GetScale();
            //producer = GameControl.control.gameData.PlantData.GetProducer();
            //consumer = GameControl.control.gameData.PlantData.GetConsumer();
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
