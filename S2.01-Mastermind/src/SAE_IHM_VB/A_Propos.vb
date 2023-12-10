Public Class A_Propos
    Private Sub A_Propos_Focus(sender As Object, e As EventArgs) Handles MyBase.LostFocus
        Me.Focus()
    End Sub

    Private Sub A_Propos_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.Focus()
    End Sub

End Class