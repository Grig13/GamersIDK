IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE TABLE [Carts] (
        [Id] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        CONSTRAINT [PK_Carts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE TABLE [News] (
        [Id] uniqueidentifier NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NULL,
        [Attachment] nvarchar(max) NULL,
        CONSTRAINT [PK_News] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE TABLE [Timelines] (
        [Id] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Timelines] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE TABLE [Products] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Price] int NOT NULL,
        [CartId] uniqueidentifier NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Products_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [Carts] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE TABLE [Posts] (
        [Id] uniqueidentifier NOT NULL,
        [PostContent] nvarchar(max) NOT NULL,
        [PostImage] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        [TimelineId] uniqueidentifier NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Posts_Timelines_TimelineId] FOREIGN KEY ([TimelineId]) REFERENCES [Timelines] ([Id]),
        CONSTRAINT [FK_Posts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE TABLE [ProductComments] (
        [Id] uniqueidentifier NOT NULL,
        [CommentContent] nvarchar(max) NOT NULL,
        [Grade] int NULL,
        [PorudctId] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NULL,
        CONSTRAINT [PK_ProductComments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ProductComments_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE TABLE [PostComments] (
        [Id] uniqueidentifier NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [PostId] uniqueidentifier NOT NULL,
        [CommentContent] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_PostComments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PostComments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE INDEX [IX_PostComments_PostId] ON [PostComments] ([PostId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE INDEX [IX_Posts_TimelineId] ON [Posts] ([TimelineId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    CREATE INDEX [IX_Products_CartId] ON [Products] ([CartId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230312100222_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230312100222_InitialMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230314184455_SecondMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230314184455_SecondMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

--IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230318112422_ThirdMigration')
--BEGIN
--    ALTER TABLE [ProductComments] DROP CONSTRAINT [FK_ProductComments_Products_ProductId];
--END;
--GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230318112422_ThirdMigration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'PorudctId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [ProductComments] DROP COLUMN [PorudctId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230318112422_ThirdMigration')
BEGIN
    -- Drop the index if it exists
    IF EXISTS(SELECT * FROM sys.indexes WHERE name = 'IX_ProductComments_ProductId' AND object_id = OBJECT_ID('ProductComments'))
    BEGIN
        DROP INDEX [IX_ProductComments_ProductId] ON [ProductComments];
    END

    -- Drop the default constraint if it exists
    DECLARE @defaultConstraintName sysname;
    SELECT @defaultConstraintName = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'ProductId');

    IF @defaultConstraintName IS NOT NULL
    BEGIN
        EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @defaultConstraintName + '];');
    END

    -- Drop the foreign key constraint if it exists
    IF EXISTS(SELECT * FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('ProductComments') AND referenced_object_id = OBJECT_ID('Products'))
    BEGIN
        ALTER TABLE [ProductComments] DROP CONSTRAINT [FK_ProductComments_Products];
    END

    -- Update existing NULL values in 'ProductId' column
    EXEC(N'UPDATE [ProductComments] SET [ProductId] = ''00000000-0000-0000-0000-000000000000'' WHERE [ProductId] IS NULL');

    -- Alter the column to NOT NULL
    ALTER TABLE [ProductComments] ALTER COLUMN [ProductId] uniqueidentifier NOT NULL;

    -- Add default constraint to 'ProductId' column
    ALTER TABLE [ProductComments] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [ProductId];

    -- Recreate the index
    CREATE INDEX [IX_ProductComments_ProductId] ON [ProductComments] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230318112422_ThirdMigration')
BEGIN
    ALTER TABLE [Carts] ADD [TotalPrice] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230318112422_ThirdMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230318112422_ThirdMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230318181414_FourthMigration')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Carts]') AND [c].[name] = N'TotalPrice');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Carts] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Carts] ALTER COLUMN [TotalPrice] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230318181414_FourthMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230318181414_FourthMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230319092037_FifthMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230319092037_FifthMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230319092454_SixthMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230319092454_SixthMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230321185739_SeventhMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230321185739_SeventhMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403161657_EigthMigration')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Carts]') AND [c].[name] = N'ProductId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Carts] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Carts] ALTER COLUMN [ProductId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403161657_EigthMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230403161657_EigthMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403171656_NinthMigration')
BEGIN
    ALTER TABLE [PostComments] DROP CONSTRAINT [FK_PostComments_Posts_PostId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403171656_NinthMigration')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PostComments]') AND [c].[name] = N'PostId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [PostComments] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [PostComments] ALTER COLUMN [PostId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403171656_NinthMigration')
BEGIN
    ALTER TABLE [PostComments] ADD CONSTRAINT [FK_PostComments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403171656_NinthMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230403171656_NinthMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230423112711_TenthMigration')
BEGIN
    ALTER TABLE [News] ADD [Title] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230423112711_TenthMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230423112711_TenthMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230424160336_eleventhmigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230424160336_eleventhmigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230424161102_twelfthmigration')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[News]') AND [c].[name] = N'Title');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [News] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [News] ALTER COLUMN [Title] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230424161102_twelfthmigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230424161102_twelfthmigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Carts_CartId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    DROP INDEX [IX_Products_CartId] ON [Products];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'CartId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Products] DROP COLUMN [CartId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'Grade');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [ProductComments] DROP COLUMN [Grade];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Carts]') AND [c].[name] = N'ProductId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Carts] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Carts] DROP COLUMN [ProductId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Carts]') AND [c].[name] = N'Quantity');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Carts] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Carts] DROP COLUMN [Quantity];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Carts]') AND [c].[name] = N'TotalPrice');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Carts] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Carts] DROP COLUMN [TotalPrice];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    ALTER TABLE [Products] ADD [Category] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    ALTER TABLE [ProductComments] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    ALTER TABLE [ProductComments] ADD [Rating] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    ALTER TABLE [Carts] ADD [UserId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    CREATE TABLE [CartItem] (
        [Id] uniqueidentifier NOT NULL,
        [CartId] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        CONSTRAINT [PK_CartItem] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CartItem_Carts_CartId] FOREIGN KEY ([CartId]) REFERENCES [Carts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CartItem_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    CREATE INDEX [IX_CartItem_CartId] ON [CartItem] ([CartId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504130921_shopmigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504130921_shopmigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504131346_shopmigrationv2')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'Rating');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [ProductComments] ALTER COLUMN [Rating] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504131346_shopmigrationv2')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'CommentContent');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [ProductComments] ALTER COLUMN [CommentContent] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504131346_shopmigrationv2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504131346_shopmigrationv2', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

--IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504131803_shopmigrationv3')
--BEGIN
--    ALTER TABLE [ProductComments] DROP CONSTRAINT [FK_ProductComments_Products_ProductId];
--END;
--GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504131803_shopmigrationv3')
BEGIN
    -- Drop the default constraint if it exists
    DECLARE @defaultConstraintName sysname;
    SELECT @defaultConstraintName = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'ProductId');

    IF @defaultConstraintName IS NOT NULL
    BEGIN
        EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @defaultConstraintName + '];');
    END

    -- Drop the foreign key constraint if it exists
    IF EXISTS(SELECT * FROM sys.foreign_keys WHERE parent_object_id = OBJECT_ID('ProductComments') AND referenced_object_id = OBJECT_ID('Products'))
    BEGIN
        ALTER TABLE [ProductComments] DROP CONSTRAINT [FK_ProductComments_Products];
    END

    -- Alter the column to allow NULL values
    ALTER TABLE [ProductComments] ALTER COLUMN [ProductId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504131803_shopmigrationv3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504131803_shopmigrationv3', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504133616_shopmigrationv4')
BEGIN
    DROP INDEX [IX_ProductComments_ProductId] ON [ProductComments];
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'ProductId');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var14 + '];');
    EXEC(N'UPDATE [ProductComments] SET [ProductId] = ''00000000-0000-0000-0000-000000000000'' WHERE [ProductId] IS NULL');
    ALTER TABLE [ProductComments] ALTER COLUMN [ProductId] uniqueidentifier NOT NULL;
    ALTER TABLE [ProductComments] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [ProductId];
    CREATE INDEX [IX_ProductComments_ProductId] ON [ProductComments] ([ProductId]);
END;
GO

--IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504133616_shopmigrationv4')
--BEGIN
--    ALTER TABLE [ProductComments] DROP CONSTRAINT [FK_ProductComments_Products_ProductId];
--END;
--GO

--IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504133616_shopmigrationv4')
--BEGIN
--    ALTER TABLE [ProductComments] ADD [ProductIdNullable] uniqueidentifier NULL;
--END;
--GO

--IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504133616_shopmigrationv4')
--BEGIN
--    UPDATE ProductComments SET ProductIdNullable = ProductId
--END;
--GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504133616_shopmigrationv4')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'ProductId');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [ProductComments] DROP COLUMN [ProductId];
END;
GO

--IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504133616_shopmigrationv4')
--BEGIN
--    EXEC sp_rename N'[ProductComments].[ProductIdNullable]', N'ProductId', N'COLUMN';
--END;
--GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504133616_shopmigrationv4')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504133616_shopmigrationv4', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504134834_shopmigrationv5')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Name');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Name] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504134834_shopmigrationv5')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Description');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Description] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504134834_shopmigrationv5')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Category');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Category] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504134834_shopmigrationv5')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504134834_shopmigrationv5', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504135101_pleaseworkimtired')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'ProductId');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [ProductComments] ALTER COLUMN [ProductId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504135101_pleaseworkimtired')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504135101_pleaseworkimtired', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

--IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504140026_DoamneAJUTA')
--BEGIN
--    ALTER TABLE [ProductComments] DROP CONSTRAINT [FK_ProductComments_Products_ProductId];
--END;
--GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504140026_DoamneAJUTA')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProductComments]') AND [c].[name] = N'ProductId');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [ProductComments] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [ProductComments] ALTER COLUMN [ProductId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504140026_DoamneAJUTA')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504140026_DoamneAJUTA', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504140559_GODHELP')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504140559_GODHELP', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230504150122_lastchance')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230504150122_lastchance', N'7.0.3');
END;
GO

COMMIT;
GO

