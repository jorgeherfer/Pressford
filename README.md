In order to login into the web app, you can use these credentials (password for all: 1234):

DemoPublisher

DemoEmployee
DemoEmployee2
DemoEmployee3

You cannot create new users directly, but you can reconfigure the context initializer (PressfordInitializer.cs).
All users can read, comment and like articles. Publishers can also modify, delete and create them.

The database should be generated when the application starts for the first time, in the app_data folder.

Given the limited amount of time (I have spent around 8 hours), I have not included interfaces, IoC or unit testing, but with more time I would have included them.
