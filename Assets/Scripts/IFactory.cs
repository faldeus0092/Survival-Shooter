// interface untuk mengimplementasikan factory
using UnityEngine;

public interface IFactory
{
    GameObject FactoryMethod(int tag);
}