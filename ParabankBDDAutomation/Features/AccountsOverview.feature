Feature: Accounts Overview
  As a customer
  I want to view account transactions
  So I can track activity and balances

  Background:
    Given Logged in successfully with valid credentials

  Scenario: View recent transactions for checking account
    When I open account "13455" from accounts overview
    Then I should be able to see transactions listed
