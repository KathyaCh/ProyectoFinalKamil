USE [Kamil]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Yogeshkumar Hadiya>
-- Description: <Perform crud operation on employee table>
-- =============================================
CREATE PROCEDURE [dbo].[Catalogo_PuestoCrudOperation]
    -- Add the parameters for the stored procedure here
    @Num_Puesto int,
    @Nombre_Puesto nvarchar(max),
    @Descripcion_Puesto nvarchar(max),
    @OperationType int
    --================================================
    --operation types
    -- 1) Insert
    -- 2) Update
    -- 3) Delete
    -- 4) Select Perticular Record
    -- 5) Selec All
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;
    --select operation
    IF @OperationType=1
    BEGIN
        INSERT INTO Catalogo_Puesto VALUES (@Nombre_Puesto,@Descripcion_Puesto)
    END
    ELSE IF @OperationType=2
    BEGIN
        UPDATE Catalogo_Puesto SET Nombre_Puesto=@Nombre_Puesto, Descripcion_Puesto=@Descripcion_Puesto WHERE Num_Puesto=@Num_Puesto
    END
    ELSE IF @OperationType=3
    BEGIN
        DELETE FROM Catalogo_Puesto  WHERE @Num_Puesto=@Num_Puesto
    END
    ELSE IF @OperationType=4
    BEGIN
        SELECT * FROM Catalogo_Puesto WHERE Num_Puesto=@Num_Puesto
    END
    ELSE
    BEGIN
        SELECT * FROM Catalogo_Puesto
    END
END