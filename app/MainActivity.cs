using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Java.Util;
using Java.Text;

namespace International_App
{
    [Activity(Label = "International_App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected TextView title, languageSelected, subtitleShortDate, subtitleLongDate, contentShortDate, contentLongDate;
        protected Spinner selectLanguage;

        protected Configuration configuration;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);       
            initializeTextViews();
            initializeSpinner();
        }

        /// <summary>
        ///     Méthode d'initialisation des TextViews
        /// </summary>
        private void initializeTextViews()
        {
            // Etape 1 : On récupère les références via la classe R
            title = FindViewById<TextView>(Resource.Id.activity_title);
            languageSelected = FindViewById<TextView>(Resource.Id.language_selected);
            subtitleShortDate = FindViewById<TextView>(Resource.Id.subtitle_date_short);
            subtitleLongDate = FindViewById<TextView>(Resource.Id.subtitle_date_long);
            contentShortDate = FindViewById<TextView>(Resource.Id.date_string_short);
            contentLongDate = FindViewById<TextView>(Resource.Id.date_string_long);

            // Etape 2 : On place le texte adéquat dans les titres et sous-titres
            title.Text = Resources.GetString(Resource.String.app_name);
            subtitleShortDate.Text = Resources.GetString(Resource.String.date_short_title);
            subtitleLongDate.Text = Resources.GetString(Resource.String.date_long_title);

            // Etape 3 : On récupère la date du jour
            Calendar calendar = Calendar.GetInstance(Locale.Default);
            Date currentDate = calendar.Time;

            // Etape 4 : On place le texte des dates à partir de la variable Locale
            contentShortDate.Text = DateFormat.GetDateInstance(DateFormat.Short, Locale.Default).Format(currentDate);
            contentLongDate.Text = DateFormat.GetDateInstance(DateFormat.Long, Locale.Default).Format(currentDate);

        }

        /// <summary>
        ///     Méthode d'initialisation du Spinner
        /// </summary>
        private void initializeSpinner()
        {

            // Etape 1 : On récupère les références via Resource
            selectLanguage = FindViewById<Spinner>(Resource.Id.spinner_country);

            // Etape 2 : On crée un adaptateurs simple contenant les valeurs de notre liste de langages disponibles
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.countries, Android.Resource.Layout.SimpleSpinnerItem);

            // Etape 3 ; On attache l'adaptateur à notre Spinner
            selectLanguage.Adapter = adapter;

            // Etape 4 : on préselecte le Spinner en fonction de la variable Locale sauvegardée
            if (Locale.Default == Locale.French)
            {
                selectLanguage.SetSelection(0, false);
            }
            else if (Locale.Default == Locale.English)
            {
                selectLanguage.SetSelection(1, false);
            }

            // Etape 5 : On crée le listener sur le Spinner
            selectLanguage.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) =>
            {
                configuration = Resources.Configuration;
                switch (e.Position)
                {
                    case 0:
                        Locale.Default = Locale.French; // On attribue à la valeur Locale par défaut le français
                        configuration.Locale = Locale.Default; // On attribue à la configuration la valeur précédemment créée
                        Resources.UpdateConfiguration(configuration, null); // On met à jour la configuration
                        StartActivity(typeof(MainActivity)); // On relance l'activité
                        break;
                    case 1:
                        Locale.Default = Locale.English; // On attribue à la valeur Locale par défaut l'anglais
                        configuration.Locale = Locale.Default; // On attribue à la configuration la valeur précédemment créée
                        Resources.UpdateConfiguration(configuration, null); // On met à jour la configuration
                        StartActivity(typeof(MainActivity)); // On relance l'activité
                        break;
                }

            };

        }

    }
}

