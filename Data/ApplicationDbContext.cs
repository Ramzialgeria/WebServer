using ClientMonitoring.Web.Data.Configuration;
using ClientMonitoring.Web.Domain.Entities;
using ClientMonitoring.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClientMonitoring.Web.Data
{
    public class ApplicationDbContext
    : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<LoginHistory> LoginHistories { get; set; }
        //public DbSet<Fonction> Fonction { get; set; }
        public DbSet<NiveauEtude> NiveauEtudes { get; set; }
        //public DbSet<Structure> Structures { get; set; }
        //public DbSet<Central> Central { get; set; }
        //public DbSet<Commune> Commune { get; set; }
        //public DbSet<Wilaya> Wilaya { get; set; }
        public DbSet<Scrutin> Scrutin { get; set; }
        public DbSet<Candidature> Candidature { get; set; }
        public DbSet<DossierCandidat> DossierCandidat { get; set; }
        public DbSet<CircoAPW> CircoAPW { get; set; }
        public DbSet<CircoAPC> CircoAPC { get; set; }
        public DbSet<Condition> Condition { get; set; }
        public DbSet<ConditionListe> ConditionListe { get; set; }
        public DbSet<ConditionCandidat> ConditionCandidat { get; set; }
        public DbSet<CandidatInfo> CandidatInfo { get; set; }
        public DbSet<PartiePolitique> PartiePolitique { get; set; }
        public DbSet<Partie> Partie { get; set; }
        public DbSet<Libre> Libre { get; set; }
        public DbSet<Union> Union { get; set; }
        public DbSet<ListeElectoral> ListeElectoral { get; set; }
        public DbSet<ListeElectoralAPW> ListeElectoralAPW { get; set; }
        public DbSet<ListeElectoralAPC> ListeElectoralAPC { get; set; }
        public DbSet<DossierListeElectoral> DossierListeElectoral { get; set; }
        public DbSet<TypeDossier> TypeDossier { get; set; }
        public DbSet<Navbar> Navbar { get; set; }
        public DbSet<NavbarType> NavbarType { get; set; }
        public DbSet<TypeConditionCandidature> TypeConditionCandidatures { get; set; }
        // features 

        public DbSet<Features> Features { get; set; }
        public DbSet<CirconscriptionFeature> CirconscriptionFeatures { get; set; }

        //added by ramzi
        public DbSet<CandidatFonction> candidatFonctions { get; set; }
        public DbSet<Representant> Representant { get; set; }
        public DbSet<RetraitDossier> RetraitDossier { get; set; }
        public DbSet<EnqueteResult> EnqueteResult { get; set; }
        public DbSet<EnqueteStructure> EnqueteStructure { get; set; }
        public DbSet<ResultCandidatHisto> ResultCandidatHisto { get; set; }
        public DbSet<ResultatListHisto> ResultatListHisto { get; set; }
        public DbSet<MotifRejet> MotifRejet { get; set; }
        public DbSet<Recours> Recours { get; set; }
        public DbSet<RecoursListe> RecoursListe { get; set; }
       
        public DbSet<Souscription> Souscription { get; set; }   
        public DbSet<TypeSouscription> TypeSouscription { get; set; }   
        public DbSet<Commission> Commission { get; set; } 
        public DbSet<TypeMutted> TypeMutted { get; set; }
        //added by toufik
        public DbSet<DesisteListe> DesisteListe { get; set; }

        //added by ramzi
        public DbSet<PvSouscription> PvSouscription { get; set; }
        public DbSet<JugementCorrespondance> JugementCorrespondance { get; set; }
        public DbSet<InfoValidation> InfoValidation { get; set; }
        public DbSet<CircoInfo> CircoInfo { get; set; }
        public DbSet<PreviousReject> PreviousReject { get; set; }
        public DbSet<Previous4percent> Previous4percent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");

            // Explicitly configure the inheritance strategy.
            modelBuilder.Entity<ListeElectoral>()
                .UseTptMappingStrategy();

            modelBuilder.ApplyConfiguration(new NiveauEtudeConfiguration());
            modelBuilder.ApplyConfiguration(new CircoAPWConfiguration());
            modelBuilder.ApplyConfiguration(new CircoAPCConfiguration());
            modelBuilder.ApplyConfiguration(new CandidatureConfiguration());
            modelBuilder.ApplyConfiguration(new DossierCandidatConfiguration());
            modelBuilder.ApplyConfiguration(new ConditionConfiguration());
            modelBuilder.ApplyConfiguration(new ConditionListeConfiguration());
            modelBuilder.ApplyConfiguration(new ConditionCandidatConfiguration());
            modelBuilder.ApplyConfiguration(new CandidatInfoConfiguration());
            modelBuilder.ApplyConfiguration(new PartiePolitiqueConfiguration());
            modelBuilder.ApplyConfiguration(new PartieConfiguration());
            modelBuilder.ApplyConfiguration(new LibreConfiguration());
            modelBuilder.ApplyConfiguration(new UnionConfiguration());
            modelBuilder.ApplyConfiguration(new ListeElectoralConfiguration());
            modelBuilder.ApplyConfiguration(new DossierListeElectoralConfiguration());
            modelBuilder.ApplyConfiguration(new TypeDossierConfiguration());
            modelBuilder.ApplyConfiguration(new ScrutinConfiguration());
            modelBuilder.ApplyConfiguration(new NavBarConfiguration());
            modelBuilder.ApplyConfiguration(new NavBarTypeConfiguration());

            //  // features configurations
            modelBuilder.ApplyConfiguration(new FeaturesConfigurations());
            modelBuilder.ApplyConfiguration(new CirconscriptionFeatureConfigurations());


            // added by ramzi
            modelBuilder.ApplyConfiguration(new RepresentantConfiguration());
            modelBuilder.ApplyConfiguration(new RetraitDossierConfiguration());
            modelBuilder.ApplyConfiguration(new EnqueteResultConfiguration());
            modelBuilder.ApplyConfiguration(new ResultCandidatHistoConfiguration());
            modelBuilder.ApplyConfiguration(new ResultatListHistoConfiguration());
            modelBuilder.ApplyConfiguration(new RecoursConfiguration());
            modelBuilder.ApplyConfiguration(new RecoursListeConfiguration());
            modelBuilder.ApplyConfiguration(new SouscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new TypeSouscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new CommissionConfiguration());
            modelBuilder.ApplyConfiguration(new TypeMuttedConfiguration());
            //added by toufik
            modelBuilder.ApplyConfiguration(new DesisteListeConfiguration());
            //added by ramzi
            modelBuilder.ApplyConfiguration(new PvSouscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new JugementCorrespondanceConfiguration());
            modelBuilder.ApplyConfiguration(new InfoValidationConfiguration());
            modelBuilder.ApplyConfiguration(new CircoInfoConfiguration());

            modelBuilder.ApplyConfiguration(new ListeElectoralAPWConfiguration());
            modelBuilder.ApplyConfiguration(new ListeElectoralAPCConfiguration());
            modelBuilder.ApplyConfiguration(new PreviousRejectConfiguration());
            modelBuilder.ApplyConfiguration(new Previous4percentConfiguration());


        }

    }
}
