using System;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBase
{
    public partial class mardevContext : DbContext
    {
        public mardevContext()
        {
        }

        public mardevContext(DbContextOptions<mardevContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Api> Api { get; set; }
        public virtual DbSet<ApiUser> ApiUser { get; set; }
        public virtual DbSet<ApiUserPlatformToken> ApiUserPlatformToken { get; set; }
        public virtual DbSet<CompleteSalesRequest> CompleteSalesRequest { get; set; }
        public virtual DbSet<CompleteSalesRequestItem> CompleteSalesRequestItem { get; set; }
        public virtual DbSet<EbayOrderTransaction> EbayOrderTransaction { get; set; }
        public virtual DbSet<LogPullOrder> LogPullOrder { get; set; }
        public virtual DbSet<QueueCompleteSales> QueueCompleteSales { get; set; }

        // Unable to generate entity type for table 'LogApiRequest'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=dev-teams-mariadb.cxkbhs9nvapa.ap-southeast-1.rds.amazonaws.com;User Id=mar-admin;Password=mar000000;Database=mar-dev");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Api>(entity =>
            {
                entity.Property(e => e.ApiId)
                    .HasColumnName("api_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiKey)
                    .IsRequired()
                    .HasColumnName("api_key")
                    .HasColumnType("char(36)");

                entity.Property(e => e.AppName)
                    .IsRequired()
                    .HasColumnName("app_name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");
            });

            modelBuilder.Entity<ApiUser>(entity =>
            {
                entity.HasIndex(e => e.ApiId)
                    .HasName("IX_ApiUser_apiId");

                entity.HasIndex(e => new { e.ApiId, e.UserKey })
                    .HasName("IX_ApiUser_apiId_userKey")
                    .IsUnique();

                entity.HasIndex(e => new { e.ApiId, e.ApiUserId, e.IsDeleted })
                    .HasName("IX_ApiUser_apiId_apiUserId_isDeleted");

                entity.Property(e => e.ApiUserId)
                    .HasColumnName("api_user_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("char(36)");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");

                entity.Property(e => e.UserKey)
                    .IsRequired()
                    .HasColumnName("user_key")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UserPrivateKey)
                    .IsRequired()
                    .HasColumnName("user_private_key")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.UserToken)
                    .IsRequired()
                    .HasColumnName("user_token")
                    .HasColumnType("text");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.CallBackUrl)
                   .HasColumnName("callback_url")
                   .HasColumnType("text");
            });

            modelBuilder.Entity<ApiUserPlatformToken>(entity =>
            {
                entity.HasIndex(e => new { e.ApiId, e.ApiUserId })
                    .HasName("IX_ApiUserPlatformToken_apiId_userId");

                entity.HasIndex(e => new { e.ApiId, e.PlatformCode, e.PlatformUserKey })
                    .HasName("UQ_ApiUserPlatformToken_apiId_platformCode_PlatformUserkey")
                    .IsUnique();

                entity.HasIndex(e => new { e.ApiId, e.ApiUserId, e.ApiUserPlatformTokenId, e.IsDeleted })
                    .HasName("IX_ApiUserPlatformToken_apiId_userId_tokenId_isDeleted");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("api_user_platform_token_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .HasColumnName("access_token")
                    .HasColumnType("text");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.ExpirationTime).HasColumnName("expiration_time");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");

                entity.Property(e => e.PlatformCode)
                    .IsRequired()
                    .HasColumnName("platform_code")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.PlatformUserKey)
                    .IsRequired()
                    .HasColumnName("platform_user_key")
                    .HasColumnType("varchar(128)");

                entity.Property(e => e.PlatformUsername)
                    .IsRequired()
                    .HasColumnName("platform_username")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<CompleteSalesRequest>(entity =>
            {
                entity.HasIndex(e => new { e.ApiId, e.ApiUserId })
                    .HasName("IX_CompleteSalesRequest_apiId_apiUserId");

                entity.Property(e => e.CompleteSalesRequestId)
                    .HasColumnName("complete_sales_request_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CallBackUrl)
                    .HasColumnName("call_back_url")
                    .HasColumnType("text");

                entity.Property(e => e.NumberOfItems)
                    .HasColumnName("number_of_items")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestBy)
                    .IsRequired()
                    .HasColumnName("request_by")
                    .HasColumnType("text");

                entity.Property(e => e.RequestOn).HasColumnName("request_on");
            });

            modelBuilder.Entity<CompleteSalesRequestItem>(entity =>
            {
                entity.HasIndex(e => new { e.ApiId, e.ApiUserId, e.CompleteSalesRequestItemId })
                    .HasName("IX_CompleteSalesRequestItem_apiId_apiUserId_requestId");

                entity.Property(e => e.CompleteSalesRequestItemId)
                    .HasColumnName("complete_sales_request_item_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CompleteSalesRequestId)
                    .IsRequired()
                    .HasColumnName("complete_sales_request_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Courier)
                    .IsRequired()
                    .HasColumnName("courier")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.EbayOrderTransactionId)
                    .IsRequired()
                    .HasColumnName("ebay_order_transaction_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ErrorCode)
                    .HasColumnName("error_code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.ErrorMessage)
                    .HasColumnName("error_message")
                    .HasColumnType("text");

                entity.Property(e => e.ShipDate).HasColumnName("ship_date");

                entity.Property(e => e.Success)
                    .HasColumnName("success")
                    .HasColumnType("biy(1)");

                entity.Property(e => e.TrackingNumber)
                    .IsRequired()
                    .HasColumnName("tracking_number")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<EbayOrderTransaction>(entity =>
            {
                entity.Property(e => e.EbayOrderTransactionId)
                    .HasColumnName("ebay_order_transaction_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiId)
                    .IsRequired()
                    .HasColumnName("api_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("api_user_platform_token_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.BuyerEmail)
                    .HasColumnName("buyer_email")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.BuyerUserId)
                    .HasColumnName("buyer_user_id")
                    .HasColumnType("text");

                entity.Property(e => e.CreateOn).HasColumnName("create_on");

                entity.Property(e => e.EbayOrderId)
                    .IsRequired()
                    .HasColumnName("ebay_order_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsHandleByApp)
                    .HasColumnName("is_handle_by_app")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsShipped)
                    .HasColumnName("is_shipped")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ItemSite)
                    .IsRequired()
                    .HasColumnName("item_site")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ItemSku)
                    .HasColumnName("item_sku")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ListingType)
                    .HasColumnName("listing_type")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.MarketPlaceTransactionNumber)
                    .IsRequired()
                    .HasColumnName("market_place_transaction_number")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.ModifyOn).HasColumnName("modify_on");

                entity.Property(e => e.Platform)
                    .HasColumnName("platform")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.QuantityPurchase)
                    .HasColumnName("quantity_purchase")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShipmentCarrierUsed)
                    .HasColumnName("shipment_carrier_used")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ShipmentTrackingNumber)
                    .HasColumnName("shipment_tracking_number")
                    .HasColumnType("varchar(255)");

                entity.Property(e => e.ShippedTime).HasColumnName("shipped_time");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TransactionPrice)
                    .HasColumnName("transaction_price")
                    .HasColumnType("decimal(18,3)");

                entity.Property(e => e.TransactionSiteId)
                    .IsRequired()
                    .HasColumnName("transaction_site_id")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.VariationSku)
                    .HasColumnName("variation_sku")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<LogPullOrder>(entity =>
            {
                entity.HasKey(e => e.PullOrderBgplogId);

                entity.Property(e => e.PullOrderBgplogId)
                    .HasColumnName("PullOrderBGPLogID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApiUserPlatformTokenId)
                    .HasColumnName("api_user_platform_token_id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProcessEndTime).HasColumnType("datetime");

                entity.Property(e => e.ProcessMessage).HasColumnType("text");

                entity.Property(e => e.ProcessStartTime).HasColumnType("datetime");

                entity.Property(e => e.ProcessStatus).HasColumnType("int(11)");
            });

            modelBuilder.Entity<QueueCompleteSales>(entity =>
            {
                entity.Property(e => e.QueueCompleteSalesId)
                    .HasColumnName("queue_complete_sales_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApiUserId)
                    .IsRequired()
                    .HasColumnName("api_user_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.CompleteSalesRequestId)
                    .IsRequired()
                    .HasColumnName("complete_sales_request_id")
                    .HasColumnType("varchar(36)");
            });
        }
    }
}
