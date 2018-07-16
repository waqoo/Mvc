// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Analyzer.Testing;
using Microsoft.AspNetCore.Mvc.Analyzers.Infrastructure;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Microsoft.AspNetCore.Mvc.Analyzers
{
    public class ApiConventionCodeFixIntegrationTest
    {
        private MvcDiagnosticAnalyzerRunner AnalyzerRunner { get; } = new MvcDiagnosticAnalyzerRunner(new ApiConventionAnalyzer());

        private CodeFixRunner CodeFixRunner => CodeFixRunner.Default;

        [Fact]
        public Task ExtractToConvention_AddsAttributesToExistingConventionMethod() => RunExtractToConventionTest();

        [Fact]
        public Task ExtractToConvention_AddsNewConventionMethodToExistingConventionType() => RunExtractToConventionTest();

        private async Task RunExtractToConventionTest([CallerMemberName] string testMethod = "")
        {
            // Arrange
            var project = GetProject(testMethod);
            var controllerDocument = project.DocumentIds[0];
            var conventionDocument = project.DocumentIds[1];

            var expectedController = Read(testMethod, "Controller.Output");
            var expectedConvention = Read(testMethod, "Convention.Output");

            // Act
            var diagnostics = await AnalyzerRunner.GetDiagnosticsAsync(project);
            var updatedSolution = await CodeFixRunner.GetChangedSolutionAsync(
                new ExtractToExistingApiConventionCodeFixProvider(),
                project.GetDocument(controllerDocument),
                diagnostics[0]);

            // Assert
            var updatedProject = updatedSolution.Projects.First();
            var actualController = await ReadDocument(updatedProject, controllerDocument);
            var actualConvention = await ReadDocument(updatedProject, conventionDocument);

            Assert.Equal(expectedController, actualController);
            Assert.Equal(expectedConvention, actualConvention);
        }

        [Fact]
        public Task ExtractToNewConvention_AddsNewTypeAndConventionMethod() => RunExtractToExtractToNewConventionTest();

        [Fact]
        public Task ExtractToNewConvention_ClonesExistingConventionTypeAndMethod() => RunExtractToExtractToNewConventionTest();

        private async Task RunExtractToExtractToNewConventionTest([CallerMemberName] string testMethod = "")
        {
            var controller = Read(testMethod, "Controller.Input");

            var expectedController = Read(testMethod, "Controller.Output");
            var expectedConvention = Read(testMethod, "Convention.Output");

            var project = DiagnosticProject.Create(GetType().Assembly, new[] { controller });
            var controllerDocument = project.DocumentIds[0];

            // Act
            var diagnostics = await AnalyzerRunner.GetDiagnosticsAsync(project);
            var updatedSolution = await CodeFixRunner.GetChangedSolutionAsync(
                new ExtractToNewApiConventionCodeFixProvider(),
                project.GetDocument(controllerDocument),
                diagnostics[0]);

            // Assert
            var updatedProject = updatedSolution.Projects.First();
            var actualController = await ReadDocument(updatedProject, controllerDocument);
            var actualConvention = await ReadDocument(updatedProject, updatedProject.DocumentIds[1]);

            Assert.Equal(expectedController, actualController);
            Assert.Equal(expectedConvention, actualConvention);
        }


        private Project GetProject(string testMethod)
        {
            var controller = Read(testMethod, "Controller.Input");
            var convention = Read(testMethod, "Convention.Input");
            
            return DiagnosticProject.Create(GetType().Assembly, new[] { controller, convention });
        }

        private string Read(string testMethod, string fileName)
        {
            var testClassName = GetType().Name;
            var filePath = Path.Combine(MvcTestSource.ProjectDirectory, "TestFiles", testClassName, testMethod, fileName + ".cs");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"TestFile {testMethod} could not be found at {filePath}.", filePath);
            }

            var fileContent = File.ReadAllText(filePath);
            return TestSource.Read(fileContent)
                .Source
                .Replace("_INPUT_", "_TEST_", StringComparison.Ordinal)
                .Replace("_OUTPUT_", "_TEST_", StringComparison.Ordinal);
        }

        private static async Task<string> ReadDocument(Project project, DocumentId documentId)
        {
            var document = project.GetDocument(documentId);
            var sourceText = await document.GetTextAsync();
            return sourceText.ToString();
        }
    }
}
