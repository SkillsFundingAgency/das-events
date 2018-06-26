BEGIN TRANSACTION t1

BEGIN TRY
	INSERT INTO [dbo].[GenericEvents]
				([Type]
				,[Payload]
				,[CreatedOn]
				,[ResourceUri]
				,[ResourceType]
				,[ResourceId])
			VALUES
				('AccountCreatedEvent'
				,'{"ResourceUri":"api/accounts/M6RB9Y"}'
				,GETDATE()
				,null
				,null
				,null)

    INSERT INTO [dbo].[GenericEvents]
				([Type]
				,[Payload]
				,[CreatedOn]
				,[ResourceUri]
				,[ResourceType]
				,[ResourceId])
			VALUES
				('AccountCreatedEvent'
				,'{"ResourceUri":"api/accounts/MWGNLK"}'
				,GETDATE()
				,null
				,null
				,null)
    INSERT INTO [dbo].[GenericEvents]
				([Type]
				,[Payload]
				,[CreatedOn]
				,[ResourceUri]
				,[ResourceType]
				,[ResourceId])
			VALUES
				('AccountCreatedEvent'
				,'{"ResourceUri":"api/accounts/VJXW9Y"}'
				,GETDATE()
				,null
				,null
				,null)
    INSERT INTO [dbo].[GenericEvents]
				([Type]
				,[Payload]
				,[CreatedOn]
				,[ResourceUri]
				,[ResourceType]
				,[ResourceId])
			VALUES
				('AccountCreatedEvent'
				,'{"ResourceUri":"api/accounts/VN9XBW"}'
				,GETDATE()
				,null
				,null
				,null)
    INSERT INTO [dbo].[GenericEvents]
				([Type]
				,[Payload]
				,[CreatedOn]
				,[ResourceUri]
				,[ResourceType]
				,[ResourceId])
			VALUES
				('AccountCreatedEvent'
				,'{"ResourceUri":"api/accounts/VRR969"}'
				,GETDATE()
				,null
				,null
				,null)
    INSERT INTO [dbo].[GenericEvents]
				([Type]
				,[Payload]
				,[CreatedOn]
				,[ResourceUri]
				,[ResourceType]
				,[ResourceId])
			VALUES
				('AccountCreatedEvent'
				,'{"ResourceUri":"api/accounts/VYNDK6"}'
				,GETDATE()
				,null
				,null
				,null)

				COMMIT TRANSACTION t1
	END TRY
BEGIN CATCH
	if @@TRANCOUNT > 0
		ROLLBACK TRANSACTION t1
END CATCH
GO
