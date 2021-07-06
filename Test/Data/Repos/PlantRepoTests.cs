using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Data.Repos;
using Entities.Entities;
using NUnit.Framework;
using Test.Base;
using Test.Builders.Entities;
using Test.Metadata;

namespace Test.Data.Repos
{
    [Category(nameof(Category.Unit))]
    public class PlantRepoTests : InMemoryDatabaseTest
    {
        #region Test Create

        [Test]
        public void Create_WithNullPlant_ShouldNotSavePlant()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var repo = new PlantRepository(context);

                // Act
                Assert.Throws<ArgumentNullException>(() => repo.Create(null),
                    "Entity of type Plant cannot be null");

                // Assert
                Assert.IsFalse(context.Plants.Any());
            }
        }

        [Test]
        public async Task Create_WithPlant_ShouldReturnCreatedPlant()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plant = new PlantBuilder()
                    .WithName("Plant")
                    .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithNotes("This is a characteristic"))
                    .WithPlantCollection(new List<PlantCollection>
                    {
                         new PlantCollectionBuilder().WithNickname("Part of the collection")
                    });

                var repo = new PlantRepository(context);
                await repo.SaveChanges();

                // Act
                var created = repo.Create(plant);

                // Assert
                Assert.AreEqual("Plant", created.Name);
                Assert.AreEqual("This is a characteristic", created.PlantCharacteristic.Notes);
                Assert.AreEqual(1, created.PlantCollection.Count);
                Assert.AreEqual("Part of the collection", created.PlantCollection.First().Nickname);
            }
        }

        [Test]
        public async Task Create_WithPlant_ShouldSavePlantInDatabase()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plant = new PlantBuilder()
                    .WithName("Plant")
                    .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithNotes("This is a characteristic"))
                    .WithPlantCollection(new List<PlantCollection>
                    {
                         new PlantCollectionBuilder().WithNickname("Part of the collection")
                    });

                var repo = new PlantRepository(context);

                // Act
                repo.Create(plant);
                await repo.SaveChanges();

                // Assert
                Assert.AreEqual(1, context.Plants.Count());
                var created = context.Plants.First();

                Assert.AreEqual("Plant", created.Name);
                Assert.AreEqual("This is a characteristic", created.PlantCharacteristic.Notes);
                Assert.AreEqual(1, created.PlantCollection.Count);
                Assert.AreEqual("Part of the collection", created.PlantCollection.First().Nickname);
            }
        }

        #endregion

        #region Test Delete

        [Test]
        public async Task Delete_WithNoPlantsInRepo_ShouldNotModifyDatabase()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var repo = new PlantRepository(context);

                // Act
                Assert.Throws<ArgumentException>(() => repo.Delete(guidOne),
                    $"No entity of type Plant with guid {guidOne} was found");
                await repo.SaveChanges();

                // Assert
                Assert.IsFalse(context.Plants.Any());
            }
        }

        [Test]
        public async Task Delete_WithNoMatchingPlantInRepo_ShouldNotDeleteExistingRecord()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plant = new PlantBuilder()
                    .WithGuid(guidOne)
                    .WithName("Plant")
                    .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithNotes("This is a characteristic"))
                    .WithPlantCollection(new List<PlantCollection>
                    {
                         new PlantCollectionBuilder().WithNickname("Part of the collection")
                    });

                context.Plants.Add(plant);
                context.SaveChanges();

                var repo = new PlantRepository(context);

                // Act
                Assert.Throws<ArgumentException>(() => repo.Delete(guidTwo),
                    $"No entity of type Plant with guid {guidTwo} was found");
                await repo.SaveChanges();

                // Assert
                Assert.AreEqual(1, context.Plants.Count());
                Assert.AreEqual(guidOne, context.Plants.First().Guid);
            }
        }

        [Test]
        public async Task Delete_WithPlantsInRepo_ShouldOnlyDeletePlantWithMatchingGuid()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plants = new List<Plant>()
                {
                     new PlantBuilder()
                        .WithGuid(guidOne).WithName("one"),
                     new PlantBuilder()
                        .WithGuid(guidTwo).WithName("two")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidTwo)),
                     new PlantBuilder()
                        .WithGuid(guidThree).WithName("three")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidThree))
                };

                context.Plants.AddRange(plants);
                context.SaveChanges();

                var repo = new PlantRepository(context);

                // Act
                repo.Delete(guidTwo);
                await repo.SaveChanges();

                // Assert
                Assert.AreEqual(2, context.Plants.Count());
                var plantsInRepo = context.Plants.ToList();

                Assert.AreEqual(guidOne, plantsInRepo[0].Guid);
                Assert.AreEqual("one", plantsInRepo[0].Name);
                Assert.AreEqual(guidThree, plantsInRepo[1].Guid);
                Assert.AreEqual("three", plantsInRepo[1].Name);
            }
        }

        #endregion

        #region Test Edit

        [Test]
        public async Task Edit_WithNoPlantsInRepo_ShouldNotModifyDatabase()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plant = new PlantBuilder();
                var repo = new PlantRepository(context);

                // Act
                Assert.Throws<ArgumentException>(() => repo.Edit(plant),
                    $"No entity of type Plant with guid {guidOne} was found");
                await repo.SaveChanges();

                // Assert
                Assert.IsFalse(context.Plants.Any());
            }
        }

        [Test]
        public async Task Edit_WithNullPlant_ShouldThrowNullArgExcpetion()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plants  = new List<Plant>()
                {
                    new PlantBuilder()
                        .WithName("one")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithNotes("note one"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder().WithNickname("nickname one")
                        }),
                    new PlantBuilder()
                        .WithGuid(guidTwo)
                        .WithName("two")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithGuid(guidTwo)
                            .WithNotes("note two"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder()
                                .WithGuid(guidTwo)
                                .WithNickname("nickname two")
                        }),
                    new PlantBuilder()
                        .WithGuid(guidThree)
                        .WithName("three")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithGuid(guidThree)
                            .WithNotes("note three"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder()
                                .WithGuid(guidThree)
                                .WithNickname("nickname three")
                        }),
                };

                context.Plants.AddRange(plants);
                context.SaveChanges();

                var repo = new PlantRepository(context);

                // Act
                Assert.Throws<ArgumentNullException>(() => repo.Edit(null),
                    "Cannot edit null entity");
                await repo.SaveChanges();

                // Assert
                Assert.AreEqual(3, context.Plants.Count());
                var plantsInDb = context.Plants.ToList();

                Assert.AreEqual(guidOne, plantsInDb[0].Guid);
                Assert.AreEqual("one", plantsInDb[0].Name);
                Assert.AreEqual(guidOne, plantsInDb[0].PlantCharacteristic.Guid);
                Assert.AreEqual("note one", plantsInDb[0].PlantCharacteristic.Notes);
                Assert.AreEqual(guidOne, plantsInDb[0].PlantCollection[0].Guid);
                Assert.AreEqual("nickname one", plantsInDb[0].PlantCollection[0].Nickname);

                Assert.AreEqual(guidTwo, plantsInDb[1].Guid);
                Assert.AreEqual("two", plantsInDb[1].Name);
                Assert.AreEqual(guidTwo, plantsInDb[1].PlantCharacteristic.Guid);
                Assert.AreEqual("note two", plantsInDb[1].PlantCharacteristic.Notes);
                Assert.AreEqual(guidTwo, plantsInDb[1].PlantCollection[0].Guid);
                Assert.AreEqual("nickname two", plantsInDb[1].PlantCollection[0].Nickname);

                Assert.AreEqual(guidThree, plantsInDb[2].Guid);
                Assert.AreEqual("three", plantsInDb[2].Name);
                Assert.AreEqual(guidThree, plantsInDb[2].PlantCharacteristic.Guid);
                Assert.AreEqual("note three", plantsInDb[2].PlantCharacteristic.Notes);
                Assert.AreEqual(guidThree, plantsInDb[2].PlantCollection[0].Guid);
                Assert.AreEqual("nickname three", plantsInDb[2].PlantCollection[0].Nickname);
            }
        }

        [Test]
        public async Task Edit_WithNoPlantWithMatchingGuidInRepo_ShouldNotEditExistingPlants()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plants  = new List<Plant>()
                {
                    new PlantBuilder()
                        .WithName("one")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithNotes("note one"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder().WithNickname("nickname one")
                        }),
                    new PlantBuilder()
                        .WithGuid(guidTwo)
                        .WithName("two")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithGuid(guidTwo)
                            .WithNotes("note two"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder()
                                .WithGuid(guidTwo)
                                .WithNickname("nickname two")
                        }),
                    new PlantBuilder()
                        .WithGuid(guidThree)
                        .WithName("three")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithGuid(guidThree)
                            .WithNotes("note three"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder()
                                .WithGuid(guidThree)
                                .WithNickname("nickname three")
                        }),
                };

                var plant = new PlantBuilder().WithGuid(guidFour);

                context.Plants.AddRange(plants);
                context.SaveChanges();

                var repo = new PlantRepository(context);

                // Act
                Assert.Throws<ArgumentException>(() => repo.Edit(plant),
                    $"No entity of type Plant with guid {guidFour} was found");
                await repo.SaveChanges();

                // Assert
                Assert.AreEqual(3, context.Plants.Count());
                var plantsInDb = context.Plants.ToList();

                Assert.AreEqual(guidOne, plantsInDb[0].Guid);
                Assert.AreEqual("one", plantsInDb[0].Name);
                Assert.AreEqual(guidOne, plantsInDb[0].PlantCharacteristic.Guid);
                Assert.AreEqual("note one", plantsInDb[0].PlantCharacteristic.Notes);
                Assert.AreEqual(guidOne, plantsInDb[0].PlantCollection[0].Guid);
                Assert.AreEqual("nickname one", plantsInDb[0].PlantCollection[0].Nickname);

                Assert.AreEqual(guidTwo, plantsInDb[1].Guid);
                Assert.AreEqual("two", plantsInDb[1].Name);
                Assert.AreEqual(guidTwo, plantsInDb[1].PlantCharacteristic.Guid);
                Assert.AreEqual("note two", plantsInDb[1].PlantCharacteristic.Notes);
                Assert.AreEqual(guidTwo, plantsInDb[1].PlantCollection[0].Guid);
                Assert.AreEqual("nickname two", plantsInDb[1].PlantCollection[0].Nickname);

                Assert.AreEqual(guidThree, plantsInDb[2].Guid);
                Assert.AreEqual("three", plantsInDb[2].Name);
                Assert.AreEqual(guidThree, plantsInDb[2].PlantCharacteristic.Guid);
                Assert.AreEqual("note three", plantsInDb[2].PlantCharacteristic.Notes);
                Assert.AreEqual(guidThree, plantsInDb[2].PlantCollection[0].Guid);
                Assert.AreEqual("nickname three", plantsInDb[2].PlantCollection[0].Nickname);
            }
        }

        [Test]
        public async Task Edit_WithPlantWithMatchingGuidInRepo_ShouldEditExistingPlant()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plants  = new List<Plant>()
                {
                    new PlantBuilder()
                        .WithName("one")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithNotes("note one"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder().WithNickname("nickname one")
                        }),
                    new PlantBuilder()
                        .WithGuid(guidTwo)
                        .WithName("two")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithGuid(guidTwo)
                            .WithNotes("note two"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder()
                                .WithGuid(guidTwo)
                                .WithNickname("nickname two")
                        }),
                    new PlantBuilder()
                        .WithGuid(guidThree)
                        .WithName("three")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithGuid(guidThree)
                            .WithNotes("note three"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder()
                                .WithGuid(guidThree)
                                .WithNickname("nickname three")
                        }),
                };

                context.Plants.AddRange(plants);
                context.SaveChanges();

                var plant = new PlantBuilder()
                    .WithGuid(guidTwo)
                    .WithName("changed two!");

                var repo = new PlantRepository(context);

                // Act
                repo.Edit(plant);
                await repo.SaveChanges();

                // Arrange
                Assert.AreEqual(3, context.Plants.Count());
                var plantsInDb = context.Plants.ToList();

                Assert.AreEqual(guidTwo, plantsInDb[1].Guid);
                Assert.AreEqual("changed two!", plantsInDb[1].Name);
                Assert.AreEqual(guidTwo, plantsInDb[1].PlantCharacteristic.Guid);
                Assert.AreEqual("note two", plantsInDb[1].PlantCharacteristic.Notes);
                Assert.AreEqual(guidTwo, plantsInDb[1].PlantCollection[0].Guid);
                Assert.AreEqual("nickname two", plantsInDb[1].PlantCollection[0].Nickname);
            }
        }

        [Test]
        public async Task Edit_WithPlantWithMatchingGuidInRepo_ShouldNotEditExistingPlantsWithNoMatchingGuid()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plants  = new List<Plant>()
                {
                    new PlantBuilder()
                        .WithName("one")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithNotes("note one"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder().WithNickname("nickname one")
                        }),
                    new PlantBuilder()
                        .WithGuid(guidTwo)
                        .WithName("two")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithGuid(guidTwo)
                            .WithNotes("note two"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder()
                                .WithGuid(guidTwo)
                                .WithNickname("nickname two")
                        }),
                    new PlantBuilder()
                        .WithGuid(guidThree)
                        .WithName("three")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithGuid(guidThree)
                            .WithNotes("note three"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder()
                                .WithGuid(guidThree)
                                .WithNickname("nickname three")
                        }),
                };

                context.Plants.AddRange(plants);
                context.SaveChanges();

                var plant = new PlantBuilder()
                    .WithGuid(guidTwo)
                    .WithName("changed two!");

                var repo = new PlantRepository(context);

                // Act
                repo.Edit(plant);
                await repo.SaveChanges();

                // Arrange
                Assert.AreEqual(3, context.Plants.Count());
                var plantsInDb = context.Plants.ToList();

                Assert.AreEqual(guidOne, plantsInDb[0].Guid);
                Assert.AreEqual("one", plantsInDb[0].Name);
                Assert.AreEqual(guidOne, plantsInDb[0].PlantCharacteristic.Guid);
                Assert.AreEqual("note one", plantsInDb[0].PlantCharacteristic.Notes);
                Assert.AreEqual(guidOne, plantsInDb[0].PlantCollection[0].Guid);
                Assert.AreEqual("nickname one", plantsInDb[0].PlantCollection[0].Nickname);

                Assert.AreEqual(guidThree, plantsInDb[2].Guid);
                Assert.AreEqual("three", plantsInDb[2].Name);
                Assert.AreEqual(guidThree, plantsInDb[2].PlantCharacteristic.Guid);
                Assert.AreEqual("note three", plantsInDb[2].PlantCharacteristic.Notes);
                Assert.AreEqual(guidThree, plantsInDb[2].PlantCollection[0].Guid);
                Assert.AreEqual("nickname three", plantsInDb[2].PlantCollection[0].Nickname);
            }
        }

        [Test]
        public async Task Edit_WithPlantWithMatchingGuidInRepo_ShouldNotEditPlantCharacteristicOrPlantCollection()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plants  = new List<Plant>()
                {
                    new PlantBuilder()
                        .WithName("one")
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                            .WithNotes("note one"))
                        .WithPlantCollection(new List<PlantCollection>()
                        {
                            new PlantCollectionBuilder().WithNickname("nickname one")
                        }),
                };

                context.Plants.AddRange(plants);
                context.SaveChanges();

                var plant = new PlantBuilder()
                    .WithName("changed one!")
                    .WithPlantCharacteristic(new PlantCharacteristicBuilder()
                        .WithNotes("changed note one!"))
                    .WithPlantCollection(new List<PlantCollection>()
                    {
                        new PlantCollectionBuilder().WithNickname("changed nickname one!")
                    });

                var repo = new PlantRepository(context);

                // Act
                repo.Edit(plant);
                await repo.SaveChanges();

                // Arrange
                Assert.AreEqual(1, context.Plants.Count());
                var plantsInDb = context.Plants.ToList();

                Assert.AreEqual(guidOne, plantsInDb[0].PlantCharacteristic.Guid);
                Assert.AreEqual("note one", plantsInDb[0].PlantCharacteristic.Notes);
                Assert.AreEqual(guidOne, plantsInDb[0].PlantCollection[0].Guid);
                Assert.AreEqual("nickname one", plantsInDb[0].PlantCollection[0].Nickname);
            }
        }

        #endregion

        #region Test Get

        [Test]
        public void Get_WithNoPlantsInRepo_ShouldReturnNull()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var repo = new PlantRepository(context);

                // Act
                var plant = repo.Get(guidOne);

                // Assert
                Assert.IsNull(plant);
            }
        }

        [Test]
        public void Get_WithNoPlantMatchingGuidInRepo_ShouldReturnNull()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plants = new List<Plant>()
                {
                    new PlantBuilder()
                        .WithGuid(guidOne)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidOne)),
                    new PlantBuilder()
                        .WithGuid(guidTwo)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidTwo)),
                    new PlantBuilder()
                        .WithGuid(guidThree)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidThree)),
                };

                context.Plants.AddRange(plants);
                context.SaveChanges();

                var repo = new PlantRepository(context);

                // Act
                var plant = repo.Get(guidFour);

                // Assert
                Assert.IsNull(plant);
            }
        }

        [Test]
        public void Get_WithPlantMatchingGuidInRepo_ShouldReturnMatchingPlant()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plants = new List<Plant>()
                {
                    new PlantBuilder()
                        .WithGuid(guidOne)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidOne)),
                    new PlantBuilder()
                        .WithGuid(guidTwo)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidTwo)),
                    new PlantBuilder()
                        .WithGuid(guidThree)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidThree)),
                };

                context.Plants.AddRange(plants);
                context.SaveChanges();

                var repo = new PlantRepository(context);

                // Act
                var plant = repo.Get(guidTwo);

                // Assert
                Assert.AreEqual(guidTwo, plant.Guid);
                Assert.AreEqual(guidTwo, plant.PlantCharacteristic.Guid);
            }
        }

        [Test]
        public void GetAll_WithNoPlantsInRepo_ShouldReturnEmptyCollection()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var repo = new PlantRepository(context);

                // Act
                var plants = repo.Get();

                // Assert
                Assert.IsFalse(plants.Any());
            }
        }

        [Test]
        public void GetAll_WithPlantsInRepo_ShouldReturnAllPlants()
        {
            using (var context = new RamosiContext(options))
            {
                // Arrange
                var plantsInRepo = new List<Plant>()
                {
                    new PlantBuilder()
                        .WithGuid(guidOne)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidOne)),
                    new PlantBuilder()
                        .WithGuid(guidTwo)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidTwo)),
                    new PlantBuilder()
                        .WithGuid(guidThree)
                        .WithPlantCharacteristic(new PlantCharacteristicBuilder().WithGuid(guidThree)),
                };

                context.Plants.AddRange(plantsInRepo);
                context.SaveChanges();

                var repo = new PlantRepository(context);

                // Act
                var plants = repo.Get();

                // Assert
                Assert.AreEqual(3, plants.Count());
                var results = plants.ToList();

                Assert.AreEqual(guidOne, results[0].Guid);
                Assert.AreEqual(guidOne, results[0].PlantCharacteristic.Guid);
                Assert.AreEqual(guidTwo, results[1].Guid);
                Assert.AreEqual(guidTwo, results[1].PlantCharacteristic.Guid);
                Assert.AreEqual(guidThree, results[2].Guid);
                Assert.AreEqual(guidThree, results[2].PlantCharacteristic.Guid);
            }
        }

        #endregion
    }
}