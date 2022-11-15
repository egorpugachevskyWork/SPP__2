using Example;
using MainPart;
using MainPart.Generators.UserGenerators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class Tests
    {
        private Faker _faker;

        private A _a;

        public Tests()
        {
            var fakerConfig = new FakerConfig();
            fakerConfig.Add<A, string, NameGenerator>(a => a.Name);
            fakerConfig.Add<A, int, AgeGenerator>(a => a.Age);
            fakerConfig.Add<B, string, NameGenerator>(b => b.Name);
            fakerConfig.Add<B, int, AgeGenerator>(b => b.hello);

            _faker = new Faker(fakerConfig);
            _a = _faker.Create<A>();
        }

        [Test]
        public void CheckGeneratorsValues()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_faker.Create<byte>(), Is.Not.EqualTo(default(byte)));
                Assert.That(_faker.Create<short>(), Is.Not.EqualTo(default(short)));
                Assert.That(_faker.Create<int>(), Is.Not.EqualTo(default(int)));
                Assert.That(_faker.Create<long>(), Is.Not.EqualTo(default(long)));

                Assert.That(_faker.Create<char>(), Is.Not.EqualTo(default(char)));

                Assert.That(_faker.Create<float>(), Is.Not.EqualTo(default(float)));
                Assert.That(_faker.Create<byte>(), Is.Not.EqualTo(default(byte)));

                Assert.That(_faker.Create<string>(), Is.Not.EqualTo(default(string)));
                Assert.That(_faker.Create<DateTime>(), Is.Not.EqualTo(default(DateTime)));




            });
        }

        [Test] 
        public void CheckForCycle()
        {
            Assert.True(_a.B.C != null, $"Cannot define cycle A-B-C-A");
        }

        [Test]
        public void CheckUserGenerators()
        {
            List<string> names = new List<string>(new NameGenerator()._names);
            Assert.Multiple(() =>
            {
                Assert.That(_a.Age >= 20 && _a.Age <= 100, $"User generator of Age wasn't used Age = {_a.Age}");
                Assert.That(names.Find(name => name.Equals(_a.Name)) is not null, $"User generator of Name wasn't used Name = {_a.Name}");
            });

        }

        [Test]
        public void CheckDllGenerators()
        {

            Assert.Multiple( () =>
            {
                Assert.That(_a.B.otherName.Split("X").Length - 1 == _a.B.otherName.Length, $"Dll generators of string wasn't used otherName = {_a.B.otherName}");
                Assert.That(_a.Id >= -20 && _a.Id <= -15, $"Dll generators of int wasn't used Id = {_a.Id}");
            });
        }

        [Test]
        public void CheckForAbsentGenerator()
        {
            Assert.That(_a.B._nope == default(uint), $"Value of uint type somehow generated _nope = {_a.B._nope}");
        }

        [Test]
        public void CheckGenerationOfGenericType()
        {

            for (int i = 0; i< _a.Numbers.Count; i++)
            {
                for (int j = 0; j < _a.Numbers[i].Count; j++)
                {
                    Assert.That(_a.Numbers[i][j] >= -20 && _a.Numbers[i][j] <= -15, $"List generator generated wrong number Numbers[{i}][{j}] = {_a.Numbers[i][j]}");
                }
            }
        }
    }
}