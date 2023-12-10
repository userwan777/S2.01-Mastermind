Public Class Accueil

    Private tabJoueurs() As Joueur
    Private Joueur1 As Joueur
    Private Joueur2 As Joueur
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tabJoueurs = Demarrage.joueurs

        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()

        ' Ajouter les joueurs à la ComboBox
        For Each joueur As Joueur In tabJoueurs
            ComboBox1.Items.Add(joueur.nom)
            ComboBox2.Items.Add(joueur.nom)
        Next
    End Sub

    Private Sub ButtonQuit_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If MsgBox("Etes-vous sûr de quitter l'application ? ", vbYesNo) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub ButtonLaunch_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If Not estValide(ComboBox1) Or Not estValide(ComboBox2) Then
            MsgBox("Il manque le nom d'au moins un des joueurs !", vbOK)
        Else
            For Each joueur As Joueur In Demarrage.joueurs
                If joueur.nom = ComboBox2.Text Then
                    Joueur2 = joueur
                Else
                    Joueur2 = createJoueur(ComboBox2.Text)
                End If

                If Not contient(Joueur2) Then
                    Joueur2 = createJoueur(ComboBox2.Text)
                    Demarrage.append(Joueur2)
                    Continue For
                End If

                If joueur.nom = ComboBox1.Text Then
                    Joueur1 = joueur
                Else
                    Joueur1 = createJoueur(ComboBox1.Text)
                End If

                If Not contient(Joueur1) Then
                    Joueur1 = createJoueur(ComboBox1.Text)
                    Demarrage.append(Joueur1)
                End If
            Next

            If IsNothing(Joueur1.nom) Then
                Joueur1 = createJoueur(ComboBox1.Text)
                Demarrage.append(Joueur1)
            End If

            If IsNothing(Joueur2.nom) Then
                Joueur2 = createJoueur(ComboBox2.Text)
                Demarrage.append(Joueur2)
            End If

            Me.Hide()
            Pattern.ShowDialog()
            Me.Form1_Load(sender, e)
        End If
    End Sub

    Private Sub Param_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Parametrage.ShowDialog()
    End Sub

    Private Sub ButtonScore_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Me.Hide()
        Stats.ShowDialog()
    End Sub

    Private Sub Accueil_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Stats.Clear()
    End Sub

    Private Function estValide(ByRef champ As ComboBox) As Boolean
        If champ.Text.Length >= 1 Then
            champ.Text = StrConv(champ.Text.Trim(), vbProperCase)
            Return True
        End If
        Return False
    End Function

    Public Function Inverser()
        Dim temp As String = ComboBox1.Text
        Dim temp2 As String = ComboBox2.Text
        ComboBox1.Text = ""
        ComboBox2.Text = temp
        ComboBox1.Text = temp2
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged, ComboBox2.TextChanged
        If ComboBox1.Text = ComboBox2.Text And ComboBox1.Text.Length <> 0 Then
            MsgBox("Vous ne pouvez pas jouer avec les deux mêmes joueurs")
            ComboBox1.Text = ""
            ComboBox2.Text = ""
        End If
    End Sub

    Public Function getJoueur1()
        Return Joueur1
    End Function

    Public Function getJoueur2()
        Return Joueur2
    End Function

    Private Function createJoueur(nom As String) As Joueur
        Dim joueur As Joueur
        joueur.nom = nom
        joueur.score = 0
        joueur.meilleurTemps = 0
        joueur.nbPartieJ1 = 0
        joueur.nbPartieJ2 = 0
        joueur.cumultemps = 0
        Return joueur
    End Function

    Private Sub PictureBox_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter, PictureBox2.MouseEnter, PictureBox3.MouseEnter
        sender.Size = New Size(70, 70)
    End Sub

    Private Sub PictureBox_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave, PictureBox2.MouseLeave, PictureBox3.MouseLeave
        sender.Size = New Size(60, 60)
    End Sub
    Private Sub Accueil_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Sauvgarde.Joueur(Demarrage.joueurs)
    End Sub

    Private Sub RèglesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RèglesToolStripMenuItem.Click
        Regles.Show()
    End Sub

    Private Sub AProposToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AProposToolStripMenuItem.Click
        A_Propos.Show()
    End Sub
End Class
