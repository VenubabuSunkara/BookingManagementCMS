ALTER TABLE CouponCodes
ADD CONSTRAINT DF_CouponCodes_CreatedBy
DEFAULT 'System' FOR [CreatedBy]







DELETE FROM [dbo].[__EFMigrationsHistory];
DELETE FROM [dbo].[AddressType];
DELETE FROM [dbo].[AspNetRoleClaims];
DELETE FROM [dbo].[AspNetRoles];
DELETE FROM [dbo].[AspNetUserClaims];
DELETE FROM [dbo].[AspNetUserLogins];
DELETE FROM [dbo].[AspNetUserRoles];
DELETE FROM [dbo].[AspNetUsers];
DELETE FROM [dbo].[AspNetUserTokens];
DELETE FROM [dbo].[BookingDetails];
DELETE FROM [dbo].[BookingOrders];
DELETE FROM [dbo].[CompanyUser];
DELETE FROM [dbo].[Configurations];
DELETE FROM [dbo].[Country];
DELETE FROM [dbo].[CouponCodes];
DELETE FROM [dbo].[CustomerAddresses];
DELETE FROM [dbo].[CustomerRelatives];
DELETE FROM [dbo].[Customers];
DELETE FROM [dbo].[Driver];
DELETE FROM [dbo].[DriverMediaMapping];
DELETE FROM [dbo].[DriverRating];
DELETE FROM [dbo].[DriverVehicle];
DELETE FROM [dbo].[DriverVehicleAvailability];
DELETE FROM [dbo].[EmailTemplates];
DELETE FROM [dbo].[Location];
DELETE FROM [dbo].[Media];
DELETE FROM [dbo].[Menus];
DELETE FROM [dbo].[PackagePolicy];
DELETE FROM [dbo].[PageContent];
DELETE FROM [dbo].[Payments];
DELETE FROM [dbo].[Permissions];
DELETE FROM [dbo].[ReviewComments];
DELETE FROM [dbo].[RoleMenuPermissions];
DELETE FROM [dbo].[RoleMenus];
DELETE FROM [dbo].[SeasonalPricing];
DELETE FROM [dbo].[Site];
DELETE FROM [dbo].[sysdiagrams];
DELETE FROM [dbo].[Taxes];
DELETE FROM [dbo].[Tour_Activate];
DELETE FROM [dbo].[Tour_Destinations];
DELETE FROM [dbo].[Tour_GuideAssignments];
DELETE FROM [dbo].[Tour_Guides];
DELETE FROM [dbo].[Tour_ItemAttribute];
DELETE FROM [dbo].[Tour_ItemType];
DELETE FROM [dbo].[Tour_ItineraryDay];
DELETE FROM [dbo].[Tour_Locations];
DELETE FROM [dbo].[Tour_PackageCategory];
DELETE FROM [dbo].[Tour_PackageItem];
DELETE FROM [dbo].[Tour_PackageItemAttributeValue];
DELETE FROM [dbo].[Tour_PackageMedia];
DELETE FROM [dbo].[Tour_PackagePolicy];
DELETE FROM [dbo].[Tour_Packages];
DELETE FROM [dbo].[Tour_Reviews];
DELETE FROM [dbo].[Vehicle];
DELETE FROM [dbo].[VehicleFeature];
DELETE FROM [dbo].[VehicleFeatureMapping];
DELETE FROM [dbo].[VehicleMediaMapping];
DELETE FROM [dbo].[VehicleRating];