SELECT * FROM Topics

INSERT INTO Topics(CreatedAt, Title, [Description], ImageUrl)
VALUES
(GETDATE(), 'Python', 'A popular dynamic, strongly-typed programming language with a focus on readability', '/images/forum/py.png'),
(GETDATE(), 'C#', 'An object-oriented programming language for building applications on the .NET Framework', '/images/forum/cs.png'),
(GETDATE(), 'Haskell', 'A popular functional programming language', '/images/forum/hs.png'),
(GETDATE(), 'JavaScript', 'Multi-paradigm language based on the ECMAScript specification', '/images/forum/js.png'),
(GETDATE(), 'Go', 'Open-source statically-typed programming language developed at Google', '/images/forum/go.png')