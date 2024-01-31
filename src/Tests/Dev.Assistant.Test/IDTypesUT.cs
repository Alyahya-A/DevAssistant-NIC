using Dev.Assistant.Common.Models;
using Dev.Assistant.App.UtilitiesForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Dev.Assistant.Test
{
    [TestClass]
    public class IDTypesUT
    {
        private ListPDSTables _service;

        public IDTypesUT()
        {
            _service = new ListPDSTables(new UtilitiesFormsHome());
        }


        [TestMethod]
        public void MicroGetHealthPassportStatus()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetHealthPassportStatus",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetHealthPassportStatus",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetHealthPassportStatus failed");
        }

        [TestMethod]
        public void MicroGetAbsherRegistrationInfo()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetAbsherRegistrationInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetAbsherRegistrationInfo",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim",
                    "Unknown"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetAbsherRegistrationInfo failed");
        }

        [TestMethod]
        public void MicroGetAlienSponsorInfo()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetAlienSponsorInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetAlienSponsorInfo",
                Tables = new List<string>
                {
                    "Alien"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetAlienSponsorInfo failed");
        }

        [TestMethod]
        public void MicroGetAlienVisaInfo()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetAlienVisaInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetAlienVisaInfo",
                Tables = new List<string>
                {
                    "Alien"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetAlienVisaInfo failed");
        }

        [TestMethod]
        public void MicroGetFirmInfo()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetFirmInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "FirmID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetFirmInfo",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Establishment"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetFirmInfo failed");
        }

        [TestMethod]
        public void MicroGetMoneyBalance()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetMoneyBalance",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetMoneyBalance",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim",
                    "Establishment"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetMoneyBalance failed");
        }

        [TestMethod]
        public void MicroGetPersonAddress()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetPersonAddress",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "ID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetPersonAddress",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim",
                    "Establishment"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetPersonAddress failed");
        }


        [TestMethod]
        public void MicroGetPersonBasicInfo()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetPersonBasicInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetPersonBasicInfo",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim",
                    "Unknown"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetPersonBasicInfo failed");
        }


        [TestMethod]
        public void MicroGetPersonBirthCertificate()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetPersonBirthCertificate",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetPersonBirthCertificate",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetPersonBirthCertificate failed");
        }


        [TestMethod]
        public void MicroGetPersonDeathCertificate()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetPersonDeathCertificate",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetPersonDeathCertificate",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetPersonDeathCertificate failed");
        }


        [TestMethod]
        public void MicroGetPersonFamilyTree()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetPersonFamilyTree",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetPersonFamilyTree",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetPersonFamilyTree failed");
        }


        [TestMethod]
        public void MicroGetPersonHajjEligibility()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetPersonHajjEligibility",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetPersonHajjEligibility",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Visitor"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetPersonHajjEligibility failed");
        }

        [TestMethod]
        public void MicroGetPersonIDInfo()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetPersonIDInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetPersonIDInfo",
                Tables = new List<string>
                {
                   "Citizen",
                    "Alien"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetPersonIDInfo failed");
        }

        [TestMethod]
        public void MicroGetPersonPreferences()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetPersonPreferences",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetPersonPreferences",
                Tables = new List<string>
                {
                   "Citizen"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetPersonPreferences failed");
        }

        [TestMethod]
        public void MicroGetTravelStatus()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetTravelStatus",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonId" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetTravelStatus",
                Tables = new List<string>
                {
                   "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetTravelStatus failed");
        }

        [TestMethod]
        public void MicroGetVisitorVisaInfoByVisitorID()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetVisitorVisaInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "VisitorID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetVisitorVisaInfo",
                Tables = new List<string>
                {
                    "Visitor"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetVisitorVisaInfoByVisitorID failed");
        }

        [TestMethod]
        public void MicroGetVisitorVisaInfoBySponsorID()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetVisitorVisaInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "SponsorID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetVisitorVisaInfo",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Establishment"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetVisitorVisaInfoBySponsorID failed");
        }

        [TestMethod]
        public void MicroGetVisitorVisaInfoByIqamaNumber()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetVisitorVisaInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "IqamaNumber" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetVisitorVisaInfo",
                Tables = new List<string>
                {
                    "Alien"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetVisitorVisaInfoByIqamaNumber failed");
        }

        [TestMethod]
        public void MicroGetWeaponsLicenseInfo()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroGetWeaponsLicenseInfo",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "ID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroGetWeaponsLicenseInfo",
                Tables = new List<string>
                {
                   "Citizen",
                    "Alien",
                    "Establishment"
                }
            };

            Assert.AreEqual(expected, actual, "MicroGetWeaponsLicenseInfo failed");
        }

        [TestMethod]
        public void MicroListAccidents()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroListAccidents",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroListAccidents",
                Tables = new List<string>
                {
                   "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim"
                }
            };

            Assert.AreEqual(expected, actual, "MicroListAccidents failed");
        }

        [TestMethod]
        public void MicroListActivityLogsByActivityOperatorID()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroListActivityLogs",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "ActivityOperatorID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroListActivityLogs",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Establishment"
                }
            };

            Assert.AreEqual(expected, actual, "MicroListActivityLogsByActivityOperatorID failed");
        }

        [TestMethod]
        public void MicroListActivityLogsBySubjectID()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroListActivityLogs",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "SubjectID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroListActivityLogs",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Visitor",
                    "Pligrim",
                    "Establishment"
                }
            };

            Assert.AreEqual(expected, actual, "MicroListActivityLogsBySubjectID failed");
        }

        [TestMethod]
        public void MicroListBusinessLabors()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroListBusinessLabors",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "ID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroListBusinessLabors",
                Tables = new List<string>
                {
                    "Citizen",
                    "Alien",
                    "Establishment"
                }
            };

            Assert.AreEqual(expected, actual, "MicroListBusinessLabors failed");
        }

        [TestMethod]
        public void MicroListChangeOccupationRequests()
        {
            // expected, T actual
            var actual = _service.ListTablesService("MicroListChangeOccupationRequests",
                         new ListTablesServiceOptions { OnlyIdTypes = true, IsTesting = true, PropName = "PersonID" });

            var expected = new PdsTableOfService
            {
                Name = "MicroListChangeOccupationRequests",
                Tables = new List<string>
                {
                    "Alien"
                }
            };

            Assert.AreEqual(expected, actual, "MicroListChangeOccupationRequests failed");
        }

    }
}
