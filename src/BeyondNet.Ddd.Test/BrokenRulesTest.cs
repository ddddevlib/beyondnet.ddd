﻿using BeyondNet.Ddd.Rules;

namespace BeyondNet.Ddd.Test
{
    [TestClass]
    public class BrokenRulesTest
    {
        [TestMethod]
        public void Add_AddsBrokenRuleToList()
        {
            // Arrange
            var brokenRules = new BrokenRules();
            var brokenRule = new BrokenRule("Property", "Message");

            // Act
            brokenRules.Add(brokenRule);

            // Assert
            var result = brokenRules.GetBrokenRules();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(brokenRule, result.First());
        }

        [TestMethod]
        public void Add_DoesNotAddDuplicateBrokenRule()
        {
            // Arrange
            var brokenRules = new BrokenRules();
            var brokenRule = new BrokenRule("Property", "Message");

            // Act
            brokenRules.Add(brokenRule);
            brokenRules.Add(brokenRule);

            // Assert
            var result = brokenRules.GetBrokenRules();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(brokenRule, result.First());
        }

        [TestMethod]
        public void Add_AddsMultipleBrokenRulesToList()
        {
            // Arrange
            var brokenRules = new BrokenRules();
            var brokenRule1 = new BrokenRule("Property1", "Message1");
            var brokenRule2 = new BrokenRule("Property2", "Message2");

            // Act
            brokenRules.Add(brokenRule1);
            brokenRules.Add(brokenRule2);

            // Assert
            var result = brokenRules.GetBrokenRules();
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(brokenRule1));
            Assert.IsTrue(result.Contains(brokenRule2));
        }

        [TestMethod]
        public void Remove_RemovesBrokenRuleFromList()
        {
            // Arrange
            var brokenRules = new BrokenRules();
            var brokenRule = new BrokenRule("Property", "Message");
            brokenRules.Add(brokenRule);

            // Act
            brokenRules.Remove(brokenRule);

            // Assert
            var result = brokenRules.GetBrokenRules();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Clear_RemovesAllBrokenRulesFromList()
        {
            // Arrange
            var brokenRules = new BrokenRules();
            var brokenRule1 = new BrokenRule("Property1", "Message1");
            var brokenRule2 = new BrokenRule("Property2", "Message2");
            brokenRules.Add(brokenRule1);
            brokenRules.Add(brokenRule2);

            // Act
            brokenRules.Clear();

            // Assert
            var result = brokenRules.GetBrokenRules();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ToString_ReturnsConcatenatedMessages()
        {
            // Arrange
            var brokenRules = new BrokenRules();
            var brokenRule1 = new BrokenRule("Property1", "Message1");
            var brokenRule2 = new BrokenRule("Property2", "Message2");
            brokenRules.Add(brokenRule1);
            brokenRules.Add(brokenRule2);

            // Act
            var result = brokenRules.ToString();

            // Assert
            Assert.AreEqual("Message1\r\nMessage2\r\n", result);
        }
    }
}
