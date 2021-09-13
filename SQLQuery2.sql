SELECT * FROM Topics

INSERT INTO Topics(CreatedAt, Title, [Description])
VALUES
(GETDATE(), 'Python', 'A popular dynamic, strongly-typed programming language with a focus on readability'),
(GETDATE(), 'C#', 'An object-oriented programming language for building applications on the .NET Framework'),
(GETDATE(), 'Haskell', 'A popular functional programming language'),
(GETDATE(), 'JavaScript', 'Multi-paradigm language based on the ECMAScript specification'),
(GETDATE(), 'Go', 'Open-source statically-typed programming language developed at Google')