using System;
using Xunit;
using OptimaFarm_TestTask.Helpers;
using System.Collections.ObjectModel;
using OptimaFarm_TestTask.Models;

namespace UnitTests
{

  
    public class CompactXMLParserAnSerialyzerTests
    {
        [Fact]
        public void XML_Parsed_Returned_True()
        {
            // Arrange

            string xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfEmploee xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Emploee>\r\n    <_id>1</_id>\r\n    <FirstName>2</FirstName>\r\n    <SecondName>3</SecondName>\r\n    <LastName>1</LastName>\r\n    <Birthday>0001-01-01T00:00:00</Birthday>\r\n    <EmploymentDate>2024-06-15T00:00:00+03:00</EmploymentDate>\r\n    <Department>2</Department>\r\n    <Position>3</Position>\r\n    <City>1</City>\r\n    <Salary>0</Salary>\r\n    <IsActive>false</IsActive>\r\n  </Emploee>\r\n</ArrayOfEmploee>\r\n";
            var oc =  new ObservableCollection<Emploee>();
            //Act
            var result = CompactXMLParser.TryDeSerialyze(xml, ref oc);


            //Assert
            Assert.True(result);

        }

        [Fact]
        public void XML_Parsed_Returned_False()
        {
            // Arrange

            string xml = "<";
            var oc = new ObservableCollection<Emploee>();
            //Act
            var result = CompactXMLParser.TryDeSerialyze(xml, ref oc);


            //Assert
            Assert.False(result);

        }

        [Fact]
        public void XML_Serialyzer_returned_XMLMarkup()
        {
            //Arrange
            var test = new Test();

            //Act
            var result = CompactXMLSerialyzer.GetXMLFromObject(test);

            //Assert
            Assert.False(string.IsNullOrEmpty(result));
            Assert.Equal("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Test xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Test1>Test</Test1>\r\n  <Test2>10</Test2>\r\n</Test>", result);
        }
    }

    public class EmploeeModelTests
    {
        [Fact]
        public void EmploeeConstructorTest_ReturnsNewEqualClass()
        {
            //Arrange
            var e = Creator.CreateTestEmploee();

            //Act
            var new_e = new Emploee(e);

            //Assert
            Assert.True(e.CustomEquals(new_e));
            Assert.NotSame(e, new_e);
        }

        [Fact]
        public void EmploeeEqualComparar_trueforidentical_falsefordifferent()
        {

            //Arrange

            var e = Creator.CreateTestEmploee();

            var e1 = Creator.CreateTestEmploee();

            var e2 = Creator.CreateTestEmploee(); 

            //Act
            e2._id += 1;

            //Assert
            Assert.False(e1.CustomEquals(e2));
            Assert.True(e.CustomEquals(e1));
        }
    }
    public class DefaultStatsModelTests
    {
        [Fact]  
        public void Stats_NotNull_ToString_Test()
        {

            //Arrange
            ObservableCollection<Emploee> oc = new ObservableCollection<Emploee>();
            string expected = "Avarage salary: 100\nMax salary: 100\nSalary expenses: 200\nCurrent number of employees: 2\nQuantity of new employees for the last half a year: 0\n";
            var stats = new DefaultStats(oc);

            //Act
            oc.Add(Creator.CreateTestEmploee());
            oc.Add(Creator.CreateTestEmploee());
            var result = stats.ToString();

            //Assert
            Assert.Equal(expected, result);

            

        }

        [Fact]  
        public void Stats_Null_ToString_Test()
        {

            //Arrange
            string expected = "Avarage salary: 0\nMax salary: 0\nSalary expenses: 0\nCurrent number of employees: 0\nQuantity of new employees for the last half a year: 0\n";
            var stats = new DefaultStats(null);

            //Act
            var result = stats.ToString();

            //Assert
            Assert.Equal(expected, result);

            

        }

        [Fact]  
        public void Stats_EmptyCollection_ToString_Test()
        {

            //Arrange
            ObservableCollection<Emploee> oc = new ObservableCollection<Emploee>();
            string expected = "Avarage salary: 0\nMax salary: 0\nSalary expenses: 0\nCurrent number of employees: 0\nQuantity of new employees for the last half a year: 0\n";
            var stats = new DefaultStats(oc);
            

            //Act
            var result = stats.ToString();

            //Assert
            Assert.Equal(expected, result);

            

        }
    }
}
