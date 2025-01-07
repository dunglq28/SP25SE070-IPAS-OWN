﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CapstoneProject_SP25_IPAS_BussinessObject.Entities;

public partial class IpasContext : DbContext
{
    public IpasContext()
    {
    }

    public IpasContext(DbContextOptions<IpasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarePlanSchedule> CarePlanSchedules { get; set; }

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<CriteriaGraftedPlant> CriteriaGraftedPlants { get; set; }

    public virtual DbSet<CriteriaHarvestType> CriteriaHarvestTypes { get; set; }

    public virtual DbSet<CriteriaType> CriteriaTypes { get; set; }

    public virtual DbSet<Criterion> Criteria { get; set; }

    public virtual DbSet<Crop> Crops { get; set; }

    public virtual DbSet<Cultivar> Cultivars { get; set; }

    public virtual DbSet<Farm> Farms { get; set; }

    public virtual DbSet<FarmCoordination> FarmCoordinations { get; set; }

    public virtual DbSet<GraftedPlant> GraftedPlants { get; set; }

    public virtual DbSet<GraftedPlantNote> GraftedPlantNotes { get; set; }

    public virtual DbSet<HarvestHistory> HarvestHistories { get; set; }

    public virtual DbSet<HarvestType> HarvestTypes { get; set; }

    public virtual DbSet<HarvestTypeHistory> HarvestTypeHistories { get; set; }

    public virtual DbSet<LandPlot> LandPlots { get; set; }

    public virtual DbSet<LandPlotCoordination> LandPlotCoordinations { get; set; }

    public virtual DbSet<LandRow> LandRows { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackageDetail> PackageDetails { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<PlantCriterion> PlantCriteria { get; set; }

    public virtual DbSet<PlantGrowthHistory> PlantGrowthHistories { get; set; }

    public virtual DbSet<PlantLot> PlantLots { get; set; }

    public virtual DbSet<PlantResource> PlantResources { get; set; }

    public virtual DbSet<Process> Processes { get; set; }

    public virtual DbSet<ProcessDatum> ProcessData { get; set; }

    public virtual DbSet<ProcessStyle> ProcessStyles { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubProcess> SubProcesses { get; set; }

    public virtual DbSet<TaskFeedback> TaskFeedbacks { get; set; }

    public virtual DbSet<TypeWork> TypeWorks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserWorkLog> UserWorkLogs { get; set; }

    public virtual DbSet<WorkLog> WorkLogs { get; set; }

    public virtual DbSet<WorkLogResource> WorkLogResources { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarePlanSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__CarePlan__9C8A5B69ED4D5A2C");

            entity.ToTable("CarePlanSchedule");

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.CarePlanId).HasColumnName("CarePlanID");
            entity.Property(e => e.DayOfWeek).HasMaxLength(50);

            entity.HasOne(d => d.CarePlan).WithMany(p => p.CarePlanSchedules)
                .HasForeignKey(d => d.CarePlanId)
                .HasConstraintName("FK__CarePlanS__CareP__2180FB33");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__ChatMess__C87C037C16C70964");

            entity.ToTable("ChatMessage");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.MessageCode).HasMaxLength(1);
            entity.Property(e => e.MessageContent).HasMaxLength(1);
            entity.Property(e => e.MessageType).HasMaxLength(1);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");

            entity.HasOne(d => d.Room).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__ChatMessa__RoomI__114A936A");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__ChatRoom__32863919D9DABA0F");

            entity.ToTable("ChatRoom");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.AiresponseId).HasColumnName("AIResponseID");
            entity.Property(e => e.RoomCode).HasMaxLength(50);
            entity.Property(e => e.RoomName).HasMaxLength(200);

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.ChatRooms)
                .HasForeignKey(d => d.CreateBy)
                .HasConstraintName("FK__ChatRoom__Create__123EB7A3");
        });

        modelBuilder.Entity<CriteriaGraftedPlant>(entity =>
        {
            entity.HasKey(e => new { e.GraftedPlantId, e.CriteriaId }).HasName("PK__Criteria__47DA5598606B21FB");

            entity.ToTable("CriteriaGraftedPlant");

            entity.Property(e => e.GraftedPlantId).HasColumnName("GraftedPlantID");
            entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");
            entity.Property(e => e.IsChecked).HasColumnName("isChecked");

            entity.HasOne(d => d.Criteria).WithMany(p => p.CriteriaGraftedPlants)
                .HasForeignKey(d => d.CriteriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CriteriaG__Crite__55F4C372");

            entity.HasOne(d => d.GraftedPlant).WithMany(p => p.CriteriaGraftedPlants)
                .HasForeignKey(d => d.GraftedPlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CriteriaG__Graft__55009F39");
        });

        modelBuilder.Entity<CriteriaHarvestType>(entity =>
        {
            entity.HasKey(e => new { e.CriteriaId, e.HarvestTypeId }).HasName("PK__Criteria__30A6B59BBF089C48");

            entity.ToTable("CriteriaHarvestType");

            entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");
            entity.Property(e => e.HarvestTypeId).HasColumnName("HarvestTypeID");
            entity.Property(e => e.IsChecked).HasColumnName("isChecked");

            entity.HasOne(d => d.Criteria).WithMany(p => p.CriteriaHarvestTypes)
                .HasForeignKey(d => d.CriteriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CriteriaHarvestType_Criteria");

            entity.HasOne(d => d.HarvestType).WithMany(p => p.CriteriaHarvestTypes)
                .HasForeignKey(d => d.HarvestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CriteriaHarvestType_HarvestType");
        });

        modelBuilder.Entity<CriteriaType>(entity =>
        {
            entity.HasKey(e => e.CriteriaTypeId).HasName("PK__Criteria__82488840F8408E4A");

            entity.ToTable("CriteriaType");

            entity.Property(e => e.CriteriaTypeId).HasColumnName("CriteriaTypeID");
            entity.Property(e => e.CriteriaTypeCode).HasMaxLength(50);
            entity.Property(e => e.CriteriaTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<Criterion>(entity =>
        {
            entity.HasKey(e => e.CriteriaId).HasName("PK__Criteria__FE6ADB2D6E962A2F");

            entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");
            entity.Property(e => e.CriteriaCode).HasMaxLength(50);
            entity.Property(e => e.CriteriaName).HasMaxLength(100);
            entity.Property(e => e.CriteriaTypeId).HasColumnName("CriteriaTypeID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");

            entity.HasOne(d => d.CriteriaType).WithMany(p => p.Criteria)
                .HasForeignKey(d => d.CriteriaTypeId)
                .HasConstraintName("FK__Criteria__Criter__3587F3E0");
        });

        modelBuilder.Entity<Crop>(entity =>
        {
            entity.HasKey(e => e.CropId).HasName("PK__Crop__92356135289FBF27");

            entity.ToTable("Crop");

            entity.Property(e => e.CropId).HasColumnName("CropID");
            entity.Property(e => e.CropCode).HasMaxLength(50);
            entity.Property(e => e.CropName).HasMaxLength(255);
            entity.Property(e => e.HarvestSeason).HasMaxLength(200);
            entity.Property(e => e.LandPlotId).HasColumnName("LandPlotID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.LandPlot).WithMany(p => p.Crops)
                .HasForeignKey(d => d.LandPlotId)
                .HasConstraintName("FK__Crop__LandPlotID__2A164134");
        });

        modelBuilder.Entity<Cultivar>(entity =>
        {
            entity.HasKey(e => e.CultivarId).HasName("PK__Cultivar__1B3A54E1495A2D98");

            entity.ToTable("Cultivar");

            entity.Property(e => e.CultivarId).HasColumnName("CultivarID");
            entity.Property(e => e.CultivarCode).HasMaxLength(50);
            entity.Property(e => e.CultivarName).HasMaxLength(50);
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.HasKey(e => e.FarmId).HasName("PK__Farm__ED7BBA9949DF5005");

            entity.ToTable("Farm");

            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.ClimateZone).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FarmCode).HasMaxLength(50);
            entity.Property(e => e.FarmName).HasMaxLength(100);
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(500)
                .HasColumnName("LogoURL");
            entity.Property(e => e.SoilType).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<FarmCoordination>(entity =>
        {
            entity.HasKey(e => e.FarmCoordinationId).HasName("PK__FarmCoor__6BD490F9FC7DDB0C");

            entity.ToTable("FarmCoordination");

            entity.Property(e => e.FarmCoordinationId).HasColumnName("FarmCoordinationID");
            entity.Property(e => e.FarmCoordinationCode).HasMaxLength(50);
            entity.Property(e => e.FarmId).HasColumnName("FarmID");

            entity.HasOne(d => d.Farm).WithMany(p => p.FarmCoordinations)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FK__FarmCoord__FarmI__1332DBDC");
        });

        modelBuilder.Entity<GraftedPlant>(entity =>
        {
            entity.HasKey(e => e.GraftedPlantId).HasName("PK__GraftedP__883CF82ACE04D6EC");

            entity.ToTable("GraftedPlant");

            entity.Property(e => e.GraftedPlantId).HasColumnName("GraftedPlantID");
            entity.Property(e => e.GraftedPlantCode).HasMaxLength(50);
            entity.Property(e => e.GraftedPlantName).HasMaxLength(100);
            entity.Property(e => e.GrowthStage).HasMaxLength(100);
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.PlantId).HasColumnName("PlantID");
            entity.Property(e => e.Status).HasMaxLength(100);

            entity.HasOne(d => d.Plan).WithMany(p => p.GraftedPlants)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__GraftedPl__PlanI__540C7B00");

            entity.HasOne(d => d.Plant).WithMany(p => p.GraftedPlants)
                .HasForeignKey(d => d.PlantId)
                .HasConstraintName("FK__GraftedPl__Plant__531856C7");
        });

        modelBuilder.Entity<GraftedPlantNote>(entity =>
        {
            entity.HasKey(e => e.GraftedPlantNoteId).HasName("PK__GraftedP__09DC04716C581995");

            entity.ToTable("GraftedPlantNote");

            entity.Property(e => e.GraftedPlantNoteId).HasColumnName("GraftedPlantNoteID");
            entity.Property(e => e.GraftedPlantId).HasColumnName("GraftedPlantID");
            entity.Property(e => e.GraftedPlantNoteName).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.NoteTaker).HasMaxLength(50);

            entity.HasOne(d => d.GraftedPlant).WithMany(p => p.GraftedPlantNotes)
                .HasForeignKey(d => d.GraftedPlantId)
                .HasConstraintName("FK_GraftedPlantNote_GraftedPlant");
        });

        modelBuilder.Entity<HarvestHistory>(entity =>
        {
            entity.HasKey(e => e.HarvestHistoryId).HasName("PK__HarvestH__F15734AD4E8E3D46");

            entity.ToTable("HarvestHistory");

            entity.Property(e => e.HarvestHistoryId).HasColumnName("HarvestHistoryID");
            entity.Property(e => e.CropId).HasColumnName("CropID");
            entity.Property(e => e.HarvestHistoryCode).HasMaxLength(50);
            entity.Property(e => e.HarvestStatus).HasMaxLength(50);

            entity.HasOne(d => d.Crop).WithMany(p => p.HarvestHistories)
                .HasForeignKey(d => d.CropId)
                .HasConstraintName("FK__HarvestHi__CropI__3E1D39E1");
        });

        modelBuilder.Entity<HarvestType>(entity =>
        {
            entity.HasKey(e => e.HarvestTypeId).HasName("PK__HarvestT__ECC6EB6D4200A381");

            entity.ToTable("HarvestType");

            entity.Property(e => e.HarvestTypeId).HasColumnName("HarvestTypeID");
            entity.Property(e => e.HarvestTypeCode).HasMaxLength(50);
            entity.Property(e => e.HarvestTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<HarvestTypeHistory>(entity =>
        {
            entity.HasKey(e => new { e.HarvestTypeId, e.HarvestHistoryId }).HasName("PK__HarvestT__23D39827B02249C9");

            entity.ToTable("HarvestTypeHistory");

            entity.Property(e => e.HarvestTypeId).HasColumnName("HarvestTypeID");
            entity.Property(e => e.HarvestHistoryId).HasColumnName("HarvestHistoryID");
            entity.Property(e => e.PlantId).HasColumnName("PlantID");

            entity.HasOne(d => d.HarvestHistory).WithMany(p => p.HarvestTypeHistories)
                .HasForeignKey(d => d.HarvestHistoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HarvestTy__Harve__40058253");

            entity.HasOne(d => d.HarvestType).WithMany(p => p.HarvestTypeHistories)
                .HasForeignKey(d => d.HarvestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HarvestTy__Harve__3F115E1A");

            entity.HasOne(d => d.Plant).WithMany(p => p.HarvestTypeHistories)
                .HasForeignKey(d => d.PlantId)
                .HasConstraintName("FK_HarvestTypeHistory_Plant");
        });

        modelBuilder.Entity<LandPlot>(entity =>
        {
            entity.HasKey(e => e.LandPlotId).HasName("PK__LandPlot__ADDF712A993D469D");

            entity.ToTable("LandPlot");

            entity.Property(e => e.LandPlotId).HasColumnName("LandPlotID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.LandPlotCode).HasMaxLength(1);
            entity.Property(e => e.LandPlotName).HasMaxLength(1);
            entity.Property(e => e.SoilType).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TargetMarket).HasMaxLength(100);

            entity.HasOne(d => d.Farm).WithMany(p => p.LandPlots)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FK__LandPlot__FarmID__2739D489");
        });

        modelBuilder.Entity<LandPlotCoordination>(entity =>
        {
            entity.HasKey(e => e.LandPlotCoordinationId).HasName("PK__LandPlot__AA2545673B1A2256");

            entity.ToTable("LandPlotCoordination");

            entity.Property(e => e.LandPlotCoordinationId).HasColumnName("LandPlotCoordinationID");
            entity.Property(e => e.LandPlotCoordinationCode).HasMaxLength(50);
            entity.Property(e => e.LandPlotId).HasColumnName("LandPlotID");

            entity.HasOne(d => d.LandPlot).WithMany(p => p.LandPlotCoordinations)
                .HasForeignKey(d => d.LandPlotId)
                .HasConstraintName("FK__LandPlotC__LandP__31B762FC");
        });

        modelBuilder.Entity<LandRow>(entity =>
        {
            entity.HasKey(e => e.LandRowId).HasName("PK__LandRow__0E72A6FA44486B16");

            entity.ToTable("LandRow");

            entity.Property(e => e.LandRowId).HasColumnName("LandRowID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Direction).HasMaxLength(50);
            entity.Property(e => e.LandPlotId).HasColumnName("LandPlotID");
            entity.Property(e => e.LandRowCode).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.LandPlot).WithMany(p => p.LandRows)
                .HasForeignKey(d => d.LandPlotId)
                .HasConstraintName("FK__LandRow__LandPlo__22751F6C");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E326456E6E2");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.IsRead).HasColumnName("isRead");
            entity.Property(e => e.Link).HasMaxLength(100);
            entity.Property(e => e.NotificationCode).HasMaxLength(50);
            entity.Property(e => e.NotificationTypeId).HasColumnName("NotificationTypeID");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .HasConstraintName("FK__Notificat__Notif__5224328E");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Notification_User");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.NotificationTypeId).HasName("PK__Notifica__299002A16017DFBB");

            entity.ToTable("NotificationType");

            entity.Property(e => e.NotificationTypeId).HasColumnName("NotificationTypeID");
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.NotificationType1)
                .HasMaxLength(50)
                .HasColumnName("NotificationType");
            entity.Property(e => e.NotificationTypeCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAF03EF8CFE");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.OrderCode).HasMaxLength(50);
            entity.Property(e => e.OrderName).HasMaxLength(100);
            entity.Property(e => e.PackageId).HasColumnName("PackageID");

            entity.HasOne(d => d.Farm).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FK__Order__FarmID__1F98B2C1");

            entity.HasOne(d => d.Package).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Order__PackageID__208CD6FA");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Package__322035EC4E1AD918");

            entity.ToTable("Package");

            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.PackageCode).HasMaxLength(50);
            entity.Property(e => e.PackageName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<PackageDetail>(entity =>
        {
            entity.HasKey(e => e.PackageDetailId).HasName("PK__PackageD__A7D8258A9962A01D");

            entity.ToTable("PackageDetail");

            entity.Property(e => e.PackageDetailId).HasColumnName("PackageDetailID");
            entity.Property(e => e.PackageDetailCode).HasMaxLength(50);
            entity.Property(e => e.PackageId).HasColumnName("PackageID");

            entity.HasOne(d => d.Package).WithMany(p => p.PackageDetails)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__PackageDe__Packa__2180FB33");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.PartnerId).HasName("PK__Partner__39FD6332E9EC0DEC");

            entity.ToTable("Partner");

            entity.Property(e => e.PartnerId).HasColumnName("PartnerID");
            entity.Property(e => e.Email).HasMaxLength(20);
            entity.Property(e => e.National).HasMaxLength(100);
            entity.Property(e => e.PartnerCode).HasMaxLength(50);
            entity.Property(e => e.PartnerName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Role).WithMany(p => p.Partners)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Partner__RoleID__22751F6C");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A58FB2F27F0");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PaymentCode).HasMaxLength(50);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .HasColumnName("TransactionID");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payment__OrderID__236943A5");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Plan__755C22D7383E2697");

            entity.ToTable("Plan");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.AssignorId).HasColumnName("AssignorID");
            entity.Property(e => e.CropId).HasColumnName("CropID");
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.LandPlotId).HasColumnName("LandPlotID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.PesticideName).HasMaxLength(255);
            entity.Property(e => e.PlanCode).HasMaxLength(50);
            entity.Property(e => e.PlantId).HasColumnName("PlantID");
            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.ResponsibleBy).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.TypeWorkId).HasColumnName("TypeWorkID");

            entity.HasOne(d => d.Assignor).WithMany(p => p.Plans)
                .HasForeignKey(d => d.AssignorId)
                .HasConstraintName("FK__Plan__AssignorID__1EA48E88");

            entity.HasOne(d => d.Crop).WithMany(p => p.Plans)
                .HasForeignKey(d => d.CropId)
                .HasConstraintName("FK__Plan__CropID__208CD6FA");

            entity.HasOne(d => d.LandPlot).WithMany(p => p.Plans)
                .HasForeignKey(d => d.LandPlotId)
                .HasConstraintName("FK__Plan__LandPlotID__1DB06A4F");

            entity.HasOne(d => d.Plant).WithMany(p => p.Plans)
                .HasForeignKey(d => d.PlantId)
                .HasConstraintName("FK__Plan__PlantID__1CBC4616");

            entity.HasOne(d => d.Process).WithMany(p => p.Plans)
                .HasForeignKey(d => d.ProcessId)
                .HasConstraintName("FK_Plan_Process");

            entity.HasOne(d => d.TypeWork).WithMany(p => p.Plans)
                .HasForeignKey(d => d.TypeWorkId)
                .HasConstraintName("FK__Plan__TypeWorkID__1F98B2C1");
        });

        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => e.PlantId).HasName("PK__Plant__98FE46BCD1431F33");

            entity.ToTable("Plant");

            entity.Property(e => e.PlantId).HasColumnName("PlantID");
            entity.Property(e => e.CultivarId).HasColumnName("CultivarID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.GrowthStage).HasMaxLength(50);
            entity.Property(e => e.HealthStatus).HasMaxLength(50);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.LandRowId).HasColumnName("LandRowID");
            entity.Property(e => e.PlantCode).HasMaxLength(50);
            entity.Property(e => e.PlantLotId).HasColumnName("PlantLotID");
            entity.Property(e => e.PlantName).HasMaxLength(50);
            entity.Property(e => e.PlantReferenceId).HasColumnName("PlantReferenceID");

            entity.HasOne(d => d.Cultivar).WithMany(p => p.Plants)
                .HasForeignKey(d => d.CultivarId)
                .HasConstraintName("FK__Plant__CultivarI__1AD3FDA4");

            entity.HasOne(d => d.LandRow).WithMany(p => p.Plants)
                .HasForeignKey(d => d.LandRowId)
                .HasConstraintName("FK__Plant__LandRowID__1BC821DD");

            entity.HasOne(d => d.PlantLot).WithMany(p => p.Plants)
                .HasForeignKey(d => d.PlantLotId)
                .HasConstraintName("FK_Plant_PlantLot");

            entity.HasOne(d => d.PlantReference).WithMany(p => p.InversePlantReference)
                .HasForeignKey(d => d.PlantReferenceId)
                .HasConstraintName("FK_Plant_Plant");
        });

        modelBuilder.Entity<PlantCriterion>(entity =>
        {
            entity.HasKey(e => new { e.PlantId, e.CriteriaId }).HasName("PK__PlantCri__5718EB0E07503B86");

            entity.Property(e => e.PlantId).HasColumnName("PlantID");
            entity.Property(e => e.CriteriaId).HasColumnName("CriteriaID");
            entity.Property(e => e.IsChecked).HasColumnName("isChecked");

            entity.HasOne(d => d.Criteria).WithMany(p => p.PlantCriteria)
                .HasForeignKey(d => d.CriteriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CriteriaT__Crite__37703C52");

            entity.HasOne(d => d.Plant).WithMany(p => p.PlantCriteria)
                .HasForeignKey(d => d.PlantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CriteriaT__Plant__367C1819");
        });

        modelBuilder.Entity<PlantGrowthHistory>(entity =>
        {
            entity.HasKey(e => e.PlantGrowthHistoryId).HasName("PK__PlantGro__8F26DC48A07090D6");

            entity.ToTable("PlantGrowthHistory");

            entity.Property(e => e.PlantGrowthHistoryId).HasColumnName("PlantGrowthHistoryID");
            entity.Property(e => e.NoteTaker).HasMaxLength(100);
            entity.Property(e => e.PlantGrowthHistoryCode).HasMaxLength(50);
            entity.Property(e => e.PlantId).HasColumnName("PlantID");

            entity.HasOne(d => d.Plant).WithMany(p => p.PlantGrowthHistories)
                .HasForeignKey(d => d.PlantId)
                .HasConstraintName("FK__PlantNote__Plant__32AB8735");
        });

        modelBuilder.Entity<PlantLot>(entity =>
        {
            entity.HasKey(e => e.PlantLotId).HasName("PK__PlantLot__58D457AB9268AE5C");

            entity.ToTable("PlantLot");

            entity.Property(e => e.PlantLotId).HasColumnName("PlantLotID");
            entity.Property(e => e.PartnerId).HasColumnName("PartnerID");
            entity.Property(e => e.PlantLotCode).HasMaxLength(50);
            entity.Property(e => e.PlantLotName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(100);

            entity.HasOne(d => d.Partner).WithMany(p => p.PlantLots)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("FK_PlantLot_Partner");
        });

        modelBuilder.Entity<PlantResource>(entity =>
        {
            entity.HasKey(e => e.PlanResourceId).HasName("PK__PlantRes__1974A13733702FAA");

            entity.ToTable("PlantResource");

            entity.Property(e => e.PlanResourceId).HasColumnName("PlanResourceID");
            entity.Property(e => e.PlanResourceCode).HasMaxLength(50);
            entity.Property(e => e.PlantGrowthHistoryId).HasColumnName("PlantGrowthHistoryID");
            entity.Property(e => e.ResourceType).HasMaxLength(255);
            entity.Property(e => e.ResourceUrl).HasColumnName("ResourceURL");

            entity.HasOne(d => d.PlantGrowthHistory).WithMany(p => p.PlantResources)
                .HasForeignKey(d => d.PlantGrowthHistoryId)
                .HasConstraintName("FK_PlantResource_PlantGrowthHistory");
        });

        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK__Process__1B39A976602F8061");

            entity.ToTable("Process");

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.FarmId).HasColumnName("FarmID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ProcessCode).HasMaxLength(50);
            entity.Property(e => e.ProcessName).HasMaxLength(100);
            entity.Property(e => e.ProcessStyleId).HasColumnName("ProcessStyleID");

            entity.HasOne(d => d.Farm).WithMany(p => p.Processes)
                .HasForeignKey(d => d.FarmId)
                .HasConstraintName("FK__Process__FarmID__32AB8735");

            entity.HasOne(d => d.ProcessStyle).WithMany(p => p.Processes)
                .HasForeignKey(d => d.ProcessStyleId)
                .HasConstraintName("FK__Process__Process__339FAB6E");
        });

        modelBuilder.Entity<ProcessDatum>(entity =>
        {
            entity.HasKey(e => e.ProcessDataId).HasName("PK__ProcessD__D954CCEB1364C6C3");

            entity.Property(e => e.ProcessDataId).HasColumnName("ProcessDataID");
            entity.Property(e => e.ProcessDataCode).HasMaxLength(50);
            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.ResourceUrl)
                .HasMaxLength(50)
                .HasColumnName("ResourceURL");
            entity.Property(e => e.SubProcessId).HasColumnName("SubProcessID");

            entity.HasOne(d => d.Process).WithMany(p => p.ProcessData)
                .HasForeignKey(d => d.ProcessId)
                .HasConstraintName("FK__ProcessDa__Proce__3493CFA7");

            entity.HasOne(d => d.SubProcess).WithMany(p => p.ProcessData)
                .HasForeignKey(d => d.SubProcessId)
                .HasConstraintName("FK__ProcessDa__SubPr__3587F3E0");
        });

        modelBuilder.Entity<ProcessStyle>(entity =>
        {
            entity.HasKey(e => e.ProcessStyleId).HasName("PK__ProcessS__4B04C14163FCA845");

            entity.ToTable("ProcessStyle");

            entity.Property(e => e.ProcessStyleId).HasColumnName("ProcessStyleID");
            entity.Property(e => e.ProcessStyleCode).HasMaxLength(50);
            entity.Property(e => e.ProcessStyleName).HasMaxLength(100);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PK__RefreshT__F5845E59652045C1");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.RefreshTokenId).HasColumnName("RefreshTokenID");
            entity.Property(e => e.RefreshTokenCode).HasMaxLength(50);
            entity.Property(e => e.RefreshTokenValue).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__367C1819");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AE891FE0C");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.IsSystem).HasColumnName("isSystem");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SubProcess>(entity =>
        {
            entity.HasKey(e => e.SubProcessId).HasName("PK__SubProce__F054A88CDB066596");

            entity.ToTable("SubProcess");

            entity.Property(e => e.SubProcessId).HasColumnName("SubProcessID");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ParentSubProcessId).HasColumnName("ParentSubProcessID");
            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");
            entity.Property(e => e.ProcessStyleId).HasColumnName("ProcessStyleID");
            entity.Property(e => e.SubProcessCode).HasMaxLength(50);
            entity.Property(e => e.SubProcessName).HasMaxLength(100);

            entity.HasOne(d => d.Process).WithMany(p => p.SubProcesses)
                .HasForeignKey(d => d.ProcessId)
                .HasConstraintName("FK__SubProces__Proce__37703C52");

            entity.HasOne(d => d.ProcessStyle).WithMany(p => p.SubProcesses)
                .HasForeignKey(d => d.ProcessStyleId)
                .HasConstraintName("FK__SubProces__Proce__3864608B");
        });

        modelBuilder.Entity<TaskFeedback>(entity =>
        {
            entity.HasKey(e => e.TaskFeedbackId).HasName("PK__TaskFeed__9CC94E192A8BDA98");

            entity.ToTable("TaskFeedback");

            entity.Property(e => e.TaskFeedbackId).HasColumnName("TaskFeedbackID");
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.TaskFeedbackCode).HasMaxLength(50);
            entity.Property(e => e.WorkLogId).HasColumnName("WorkLogID");

            entity.HasOne(d => d.Manager).WithMany(p => p.TaskFeedbacks)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__TaskFeedb__Manag__395884C4");

            entity.HasOne(d => d.WorkLog).WithMany(p => p.TaskFeedbacks)
                .HasForeignKey(d => d.WorkLogId)
                .HasConstraintName("FK__TaskFeedb__WorkL__339FAB6E");
        });

        modelBuilder.Entity<TypeWork>(entity =>
        {
            entity.HasKey(e => e.TypeWorkId).HasName("PK__TypeWork__E9F5EE11E5C9B62E");

            entity.ToTable("TypeWork");

            entity.Property(e => e.TypeWorkId).HasColumnName("TypeWorkID");
            entity.Property(e => e.BackgroundColor).HasMaxLength(50);
            entity.Property(e => e.TextColor).HasMaxLength(50);
            entity.Property(e => e.TypeWorkCode).HasMaxLength(50);
            entity.Property(e => e.TypeWorkName).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC60BA6199");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UserCode).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User__RoleID__3B40CD36");

            entity.HasMany(d => d.Farms).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFarm",
                    r => r.HasOne<Farm>().WithMany()
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserFarm__FarmID__3C34F16F"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserFarm__UserID__3D2915A8"),
                    j =>
                    {
                        j.HasKey("UserId", "FarmId").HasName("PK__UserFarm__995F7705E01214A8");
                        j.ToTable("UserFarm");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<int>("FarmId").HasColumnName("FarmID");
                    });
        });

        modelBuilder.Entity<UserWorkLog>(entity =>
        {
            entity.HasKey(e => new { e.WorkLogId, e.UserId }).HasName("PK__UserWork__2F2CA10852B78F7B");

            entity.ToTable("UserWorkLog");

            entity.Property(e => e.WorkLogId).HasColumnName("WorkLogID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserWorkLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserWorkL__UserI__3E1D39E1");

            entity.HasOne(d => d.WorkLog).WithMany(p => p.UserWorkLogs)
                .HasForeignKey(d => d.WorkLogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserWorkL__WorkL__25518C17");
        });

        modelBuilder.Entity<WorkLog>(entity =>
        {
            entity.HasKey(e => e.WorkLogId).HasName("PK__WorkLog__FE542DC2D9DBD782");

            entity.ToTable("WorkLog");

            entity.Property(e => e.WorkLogId).HasColumnName("WorkLogID");
            entity.Property(e => e.CropId).HasColumnName("CropID");
            entity.Property(e => e.HarvestHistoryId).HasColumnName("HarvestHistoryID");
            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.WorkLogCode).HasMaxLength(50);

            entity.HasOne(d => d.Crop).WithMany(p => p.WorkLogs)
                .HasForeignKey(d => d.CropId)
                .HasConstraintName("FK__WorkLog__CropID__245D67DE");

            entity.HasOne(d => d.HarvestHistory).WithMany(p => p.WorkLogs)
                .HasForeignKey(d => d.HarvestHistoryId)
                .HasConstraintName("FK_WorkLog_HarvestHistory");

            entity.HasOne(d => d.Schedule).WithMany(p => p.WorkLogs)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK__WorkLog__Schedul__236943A5");
        });

        modelBuilder.Entity<WorkLogResource>(entity =>
        {
            entity.HasKey(e => e.WorkLogResourceId).HasName("PK__WorkLogR__2EE578CAC6A9B9C8");

            entity.ToTable("WorkLogResource");

            entity.Property(e => e.WorkLogResourceId).HasColumnName("WorkLogResourceID");
            entity.Property(e => e.ResourceType).HasMaxLength(255);
            entity.Property(e => e.ResourceUrl).HasColumnName("ResourceURL");
            entity.Property(e => e.WorkLogId).HasColumnName("WorkLogID");
            entity.Property(e => e.WorkLogResourceCode).HasMaxLength(50);

            entity.HasOne(d => d.WorkLog).WithMany(p => p.WorkLogResources)
                .HasForeignKey(d => d.WorkLogId)
                .HasConstraintName("FK__WorkLogRe__WorkL__29221CFB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
