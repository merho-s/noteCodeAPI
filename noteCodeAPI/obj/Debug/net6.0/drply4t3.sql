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

CREATE TABLE [codetag] (
    [id] int NOT NULL IDENTITY,
    [name] nvarchar(max) NULL,
    CONSTRAINT [PK_codetag] PRIMARY KEY ([id])
);
GO

CREATE TABLE [users] (
    [id] int NOT NULL IDENTITY,
    [username] nvarchar(max) NULL,
    [password] nvarchar(max) NULL,
    CONSTRAINT [PK_users] PRIMARY KEY ([id])
);
GO

CREATE TABLE [note] (
    [id] int NOT NULL IDENTITY,
    [title] nvarchar(max) NULL,
    [description] nvarchar(max) NULL,
    [image] nvarchar(max) NULL,
    [code] nvarchar(max) NULL,
    [user_id] int NOT NULL,
    CONSTRAINT [PK_note] PRIMARY KEY ([id]),
    CONSTRAINT [FK_note_users_user_id] FOREIGN KEY ([user_id]) REFERENCES [users] ([id]) ON DELETE CASCADE
);
GO

CREATE TABLE [notes_tags] (
    [id] int NOT NULL IDENTITY,
    [note_id] int NOT NULL,
    [tag_id] int NOT NULL,
    CONSTRAINT [PK_notes_tags] PRIMARY KEY ([id]),
    CONSTRAINT [FK_notes_tags_codetag_tag_id] FOREIGN KEY ([tag_id]) REFERENCES [codetag] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_notes_tags_note_note_id] FOREIGN KEY ([note_id]) REFERENCES [note] ([id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_note_user_id] ON [note] ([user_id]);
GO

CREATE INDEX [IX_notes_tags_note_id] ON [notes_tags] ([note_id]);
GO

CREATE INDEX [IX_notes_tags_tag_id] ON [notes_tags] ([tag_id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230203153418_m0', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230203154050_m1', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230203154400_m2', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [note] DROP CONSTRAINT [FK_note_users_user_id];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[note]') AND [c].[name] = N'user_id');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [note] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [note] ALTER COLUMN [user_id] int NULL;
GO

ALTER TABLE [note] ADD CONSTRAINT [FK_note_users_user_id] FOREIGN KEY ([user_id]) REFERENCES [users] ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230203154739_m3', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [unused_active_token] (
    [id] int NOT NULL IDENTITY,
    [token] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_unused_active_token] PRIMARY KEY ([id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230214093407_m4', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [unused_active_token] ADD [expiration_date] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230214141801_m5', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [users] ADD [role] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230215181320_m6', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[note]') AND [c].[name] = N'code');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [note] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [note] DROP COLUMN [code];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[note]') AND [c].[name] = N'image');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [note] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [note] DROP COLUMN [image];
GO

CREATE TABLE [code_snippet] (
    [id] int NOT NULL IDENTITY,
    [code] nvarchar(max) NULL,
    [description] nvarchar(max) NULL,
    [note_id] int NOT NULL,
    CONSTRAINT [PK_code_snippet] PRIMARY KEY ([id]),
    CONSTRAINT [FK_code_snippet_note_note_id] FOREIGN KEY ([note_id]) REFERENCES [note] ([id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_code_snippet_note_id] ON [code_snippet] ([note_id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230307134358_m7', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [code_snippet] ADD [language] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230311190222_m8', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [codetag_alias] (
    [id] int NOT NULL IDENTITY,
    [name] nvarchar(max) NOT NULL,
    [codetag_id] int NOT NULL,
    CONSTRAINT [PK_codetag_alias] PRIMARY KEY ([id]),
    CONSTRAINT [FK_codetag_alias_codetag_codetag_id] FOREIGN KEY ([codetag_id]) REFERENCES [codetag] ([id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_codetag_alias_codetag_id] ON [codetag_alias] ([codetag_id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230311214914_m9', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[code_snippet]') AND [c].[name] = N'language');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [code_snippet] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [code_snippet] DROP COLUMN [language];
GO

ALTER TABLE [code_snippet] ADD [LanguageId] int NULL;
GO

CREATE INDEX [IX_code_snippet_LanguageId] ON [code_snippet] ([LanguageId]);
GO

ALTER TABLE [code_snippet] ADD CONSTRAINT [FK_code_snippet_codetag_alias_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [codetag_alias] ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230312124720_m10', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230312134410_m11', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230312134526_m12', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [code_snippet] DROP CONSTRAINT [FK_code_snippet_codetag_alias_LanguageId];
GO

EXEC sp_rename N'[code_snippet].[LanguageId]', N'language_id', N'COLUMN';
GO

EXEC sp_rename N'[code_snippet].[IX_code_snippet_LanguageId]', N'IX_code_snippet_language_id', N'INDEX';
GO

ALTER TABLE [code_snippet] ADD CONSTRAINT [FK_code_snippet_codetag_alias_language_id] FOREIGN KEY ([language_id]) REFERENCES [codetag_alias] ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230312190853_m13', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [code_snippet] DROP CONSTRAINT [FK_code_snippet_codetag_alias_language_id];
GO

EXEC sp_rename N'[code_snippet].[language_id]', N'tag_alias_id', N'COLUMN';
GO

EXEC sp_rename N'[code_snippet].[IX_code_snippet_language_id]', N'IX_code_snippet_tag_alias_id', N'INDEX';
GO

ALTER TABLE [code_snippet] ADD CONSTRAINT [FK_code_snippet_codetag_alias_tag_alias_id] FOREIGN KEY ([tag_alias_id]) REFERENCES [codetag_alias] ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230312205541_m14', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230312210838_m15', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [code_snippet] DROP CONSTRAINT [FK_code_snippet_codetag_alias_tag_alias_id];
GO

EXEC sp_rename N'[code_snippet].[tag_alias_id]', N'TagAliasId', N'COLUMN';
GO

EXEC sp_rename N'[code_snippet].[IX_code_snippet_tag_alias_id]', N'IX_code_snippet_TagAliasId', N'INDEX';
GO

ALTER TABLE [code_snippet] ADD [language] nvarchar(max) NULL;
GO

ALTER TABLE [code_snippet] ADD CONSTRAINT [FK_code_snippet_codetag_alias_TagAliasId] FOREIGN KEY ([TagAliasId]) REFERENCES [codetag_alias] ([id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230312215243_m16', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230313100000_m17', N'7.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [code_snippet] DROP CONSTRAINT [FK_code_snippet_codetag_alias_TagAliasId];
GO

DROP INDEX [IX_code_snippet_TagAliasId] ON [code_snippet];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[code_snippet]') AND [c].[name] = N'TagAliasId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [code_snippet] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [code_snippet] DROP COLUMN [TagAliasId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230313100443_m18', N'7.0.2');
GO

COMMIT;
GO

